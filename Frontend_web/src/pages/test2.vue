<template>
  <!-- Container chính -->
  <div class="flex h-screen overflow-hidden bg-gray-100">

    <!-- SIDEBAR (Danh sách chat) -->
    <aside
      class="w-full md:w-1/4 flex flex-col bg-white border-r border-gray-200 shadow-sm"
      :class="[
        // Mobile: ẩn sidebar nếu đang hiển thị khung chat
        isChatActive ? 'hidden' : 'flex',
        // Desktop: luôn hiển thị
        'md:flex'
      ]"
    >
      <!-- Header sidebar -->
      <div class="px-4 py-4 border-b border-gray-200 flex items-center justify-between">
        <h1 class="text-xl font-semibold text-gray-800">Messages</h1>
      </div>

      <!-- Search box -->
      <div class="p-4 border-b border-gray-200">
        <div class="relative">
          <svg
            class="absolute left-3 top-2 h-4 w-4 text-gray-400"
            fill="none" viewBox="0 0 24 24" stroke="currentColor"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M8 16a6 6 0 1111.196 1.705l3.347 3.347a1 1 0 01-1.414 1.414l-3.347-3.347A6 6 0 018 16z"
            />
          </svg>
          <input
            type="text"
            placeholder="Search messages..."
            class="w-full pl-9 pr-4 py-2 rounded-full border border-gray-300 bg-white
                   focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
      </div>

      <!-- Danh sách chat -->
      <div class="flex-1 overflow-y-auto">
        <div
          v-for="item in store.message"
          :key="item.receiverId"
          class="flex items-center px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors duration-150"
          @click="handleItemClick(item.receiverId)"
        >
          <img
            :src="item.avatar"
            alt="Avatar"
            class="w-10 h-10 rounded-full object-cover"
          />
          <div class="ml-3 flex-1">
            <div class="flex justify-between items-center">
              <p class="text-gray-800 font-medium">
                {{ item.receiverName }}
              </p>
              <span class="text-xs text-gray-400">
                {{ item.time }}
              </span>
            </div>
            <p class="text-sm text-gray-500 truncate">
              {{ item.lastMessage }}
            </p>
          </div>
        </div>
      </div>
    </aside>

    <!-- MAIN CHAT -->
    <main
      class="flex-1 flex flex-col bg-white relative"
      :class="[
        // Mobile: hiện khung chat khi chọn
        isChatActive ? 'flex' : 'hidden',
        // Desktop: luôn hiển thị
        'md:flex'
      ]"
    >
      <!-- Header chat -->
      <div class="flex items-center justify-between px-4 py-4 border-b border-gray-200">
        <!-- Nút Back (chỉ hiện trên mobile) -->
        <button
          class="text-gray-600 mr-2 md:hidden flex items-center space-x-1"
          @click="goBackToList"
          v-if="selectedChat"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5"
               fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                  d="M15 19l-7-7 7-7" />
          </svg>
          <span>Back</span>
        </button>

        <!-- Nếu đã chọn 1 chat -->
        <div class="flex items-center space-x-4" v-if="selectedChat">
          <img
            :src="selectedChat.avatar"
            alt="Avatar"
            class="w-10 h-10 rounded-full object-cover"
          />
          <div>
            <h2 class="text-lg font-semibold text-gray-800">
              {{ selectedChat.receiverName }}
            </h2>
            <!-- Ví dụ hiển thị mô tả nhỏ (nếu muốn) -->
            <p class="text-sm text-gray-500">Designer candidate</p>
          </div>
        </div>

        <!-- Nếu chưa chọn chat nào -->
        <div v-else>
          <h2 class="text-lg font-semibold text-gray-800">
            Choose a chat
          </h2>
        </div>

        <!-- Ví dụ thêm nút 3 chấm (không có logic) -->
        <button
          class="text-gray-500 hover:text-gray-700 transition-colors duration-150"
          v-if="selectedChat"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none"
               viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                  d="M6.75 12a.75.75 0 100-1.5.75.75 0 000 1.5zm5.25 0a.75.75 0 100-1.5.75.75 0 000 1.5zm5.25 0a.75.75 0 100-1.5.75.75 0 000 1.5z" />
          </svg>
        </button>
      </div>

      <!-- Danh sách tin nhắn -->
      <div class="flex-1 overflow-y-auto p-4 space-y-4 bg-gray-50">
        <template v-for="(msg, index) in store.messageId" :key="index">
          <!-- Tin nhắn bên trái (other) -->
          <div v-if="msg.sender === 'other'" class="flex items-start space-x-3">
            <img
              :src="'https://i.pravatar.cc/40?img=1'"
              alt="Avatar"
              class="w-8 h-8 rounded-full object-cover"
            />
            <div class="max-w-lg">
              <div class="bg-white rounded-xl p-3 shadow-sm border border-gray-100">
                <p class="text-sm text-gray-800 whitespace-pre-wrap">
                  {{ msg.content }}
                </p>
              </div>
              <span class="text-xs text-gray-400 mt-1 block">{{ msg.time }}</span>
            </div>
          </div>

          <!-- Tin nhắn bên phải (mình) -->
          <div v-else class="flex items-start space-x-3 flex-row-reverse">
            <img
              :src="'https://i.pravatar.cc/40?img=4'"
              alt="Avatar"
              class="w-8 h-8 rounded-full object-cover"
            />
            <div class="max-w-lg">
              <div class="bg-blue-500 text-white rounded-xl p-3 shadow-sm mr-3">
                <p class="text-sm whitespace-pre-wrap">
                  {{ msg.content }}
                </p>
              </div>
              <span class="text-xs text-gray-400 mt-1 block">{{ msg.time }}</span>
            </div>
          </div>
        </template>
      </div>

      <!-- Input soạn tin nhắn -->
      <div class="border-t border-gray-200 p-4 bg-white">
        <div class="flex items-center space-x-2">
          <!-- Gợi ý: Thêm icon đính kèm, emoji (không có logic) -->
          <button class="text-gray-400 hover:text-gray-600 transition-colors duration-150">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none"
                 viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M15.172 7l-6.586 6.586a2 2 0 102.828 2.828L18 9.828V7h-2.828z" />
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M5 19l-2 2m14-2l2 2" />
            </svg>
          </button>

          <!-- <input
            type="text"
            placeholder="Type a message..."
            class="flex-1 p-3 rounded-full border border-gray-300 bg-white
                   focus:outline-none focus:ring-2 focus:ring-blue-500"
          /> -->
          <BaseControlTextArea
          v-model="myText"
            placeholder="Type a message..."
          />
          <button
            class="bg-blue-500 hover:bg-blue-600 text-white p-3 rounded-full
                   focus:outline-none transition-colors duration-150"
                   @click="console.log(myText)"
          >
            <!-- Icon send -->
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="h-5 w-5"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M14.752 11.168l-9.197-5.132A.75.75 0 004 6.608v10.784a.75.75 0 001.555.245l9.197-5.132a.75.75 0 000-1.337z"
              />
            </svg>
          </button>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup lang="ts">
import BaseControlTextArea from '@PKG_SRC/components/Basecontrol/BaseControlTextArea.vue'
import { useTest2Store } from '@PKG_SRC/stores/Modules/Mypg/test2'
import { ref, onMounted, computed } from 'vue'

const store = useTest2Store()

// Biến xác định đang hiển thị khung chat trên mobile
const isChatActive = ref(false)
const myText = ref('')
// Chat đang được chọn
const selectedChatId = ref<string | null>(null)
const selectedChat = computed(() => {
  if (!selectedChatId.value) return null
  return store.message.find((item) => item.receiverId === selectedChatId.value) || null
})

// Khi bấm chọn 1 item chat
function handleItemClick(id: string) {
  store.GetMessageById(id)
  selectedChatId.value = id
  // Trên mobile => ẩn sidebar, hiện khung chat
  isChatActive.value = true
}

// Nút back (chỉ mobile)
function goBackToList() {
  isChatActive.value = false
}

// Lấy danh sách chat khi vừa vào
onMounted(() => {
  store.GetMessage()
})
</script>

<style scoped>
/* Tuỳ chỉnh thêm nếu cần */
</style>
