<template>
  <!-- Container chính -->
  <div class="flex h-screen overflow-hidden bg-gray-100">
    <!-- SIDEBAR (Danh sách chat) -->
    <aside class="w-full md:w-1/4 flex flex-col bg-white border-r border-gray-200 shadow-sm" :class="[isChatActive ? 'hidden' : 'flex', 'md:flex']">
      <!-- Header sidebar -->
      <div class="px-4 py-4 border-b border-gray-200 flex items-center justify-between">
        <h1 class="text-xl font-semibold text-gray-800">Messages</h1>
      </div>

      <!-- Search box -->
      <div class="p-4 border-b border-gray-200">
        <div class="relative">
          <svg class="absolute left-3 top-2 h-4 w-4 text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
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
            class="w-full pl-9 pr-4 py-2 rounded-full border border-gray-300 bg-white focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
      </div>

      <!-- Danh sách chat -->
      <div class="flex-1 overflow-y-auto">
        <div
          v-for="item in store.conversations"
          :key="item.receiverId"
          class="flex items-center px-4 py-3 transition-colors duration-150 border-l-4"
          @click="handleItemClick(item.receiverId)"
          :class="item.unreadCount > 0 ? 'bg-gray-200 hover:bg-blue-50 border-blue-400' : 'hover:bg-gray-50 border-transparent'"
        >
          <img :src="item.avatar" alt="Avatar" class="w-10 h-10 rounded-full object-cover" />
          <div class="ml-3 flex-1">
            <div class="flex justify-between items-center">
              <!-- In đậm nếu unreadCount > 0 -->
              <p
                :class="{
                  'text-gray-800 font-bold': item.unreadCount > 0,
                  'text-gray-800 font-medium': item.unreadCount === 0,
                }"
              >
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
    <main class="flex-1 flex flex-col bg-white relative" :class="[isChatActive ? 'flex' : 'hidden', 'md:flex']">
      <!-- Header chat -->
      <div class="flex items-center justify-between px-4 py-4 border-b border-gray-200">
        <!-- Nút Back (mobile) -->
        <button class="text-gray-600 mr-2 md:hidden flex items-center space-x-1" @click="goBackToList" v-if="selectedChat">
          <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7" />
          </svg>
          <span>Back</span>
        </button>

        <!-- Nếu đã chọn 1 chat -->
        <div class="flex items-center space-x-4" v-if="selectedChat">
          <img :src="selectedChat.avatar" alt="Avatar" class="w-10 h-10 rounded-full object-cover" />
          <div>
            <h2 class="text-lg font-semibold text-gray-800">
              {{ selectedChat.receiverName }}
            </h2>
            <p class="text-sm text-gray-500">Designer candidate</p>
          </div>
        </div>

        <!-- Nếu chưa chọn chat nào -->
        <div v-else>
          <h2 class="text-lg font-semibold text-gray-800">Choose a chat</h2>
        </div>

        <button class="text-gray-500 hover:text-gray-700 transition-colors duration-150" v-if="selectedChat">
          <!-- icon 3 chấm -->
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M6.75 12a.75.75 0 100-1.5.75.75 0 000 1.5zm5.25 0a.75.75 0 100-1.5.75.75 0 000 1.5zm5.25 0a.75.75 0 100-1.5.75.75 0 000 1.5z"
            />
          </svg>
        </button>
      </div>

      <!-- Danh sách tin nhắn -->
      <!-- Thêm ref="messagesContainer" để auto-scroll -->
      <div class="flex-1 overflow-y-auto p-4 space-y-4 bg-gray-50" ref="messagesContainer">
        <template v-for="(msg, index) in currentMessages" :key="index">
          <!-- Tin nhắn bên trái (other) -->
          <div v-if="msg.sender === 'other'" class="flex items-start space-x-3">
            <img :src="'https://i.pravatar.cc/40?img=1'" alt="Avatar" class="w-8 h-8 rounded-full object-cover" />
            <div class="max-w-lg">
              <div class="bg-white rounded-xl p-3 shadow-sm border border-gray-100">
                <p class="text-sm text-gray-800 whitespace-pre-wrap">
                  {{ msg.content }}
                </p>
              </div>
              <span class="text-xs text-gray-400 mt-1 block">
                {{ msg.time }}
              </span>
            </div>
          </div>

          <!-- Tin nhắn bên phải (mine) -->
          <div v-else class="flex items-start space-x-3 flex-row-reverse">
            <img :src="'https://i.pravatar.cc/40?img=4'" alt="Avatar" class="w-8 h-8 rounded-full object-cover" />
            <div class="max-w-lg">
              <div class="bg-blue-500 text-white rounded-xl p-3 shadow-sm mr-3">
                <p class="text-sm whitespace-pre-wrap">
                  {{ msg.content }}
                </p>
              </div>
              <span class="text-xs text-gray-400 mt-1 block">
                {{ msg.time }}
              </span>
            </div>
          </div>
        </template>
      </div>

      <!-- Input soạn tin nhắn -->
      <div class="border-t border-gray-200 p-4 bg-white">
        <div class="flex items-center space-x-2">
          <button class="text-gray-400 hover:text-gray-600 transition-colors duration-150">
            <!-- Icon đính kèm -->
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M15.172 7l-6.586 6.586a2 2 0 102.828 2.828L18 9.828V7h-2.828z"
              />
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 19l-2 2m14-2l2 2" />
            </svg>
          </button>

          <!-- TextArea có @keydown.enter.prevent -->
          <BaseControlTextArea v-model="myText" placeholder="Type a message..." @keydown.enter.prevent="sendCurrentMessage" />

          <button
            class="bg-blue-500 hover:bg-blue-600 text-white p-3 rounded-full focus:outline-none transition-colors duration-150"
            @click="sendCurrentMessage"
          >
            <!-- Icon send -->
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
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
  import { ref, onMounted, computed, watch, nextTick } from 'vue'
  import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr'
  import { useTest2Store } from '@PKG_SRC/stores/Modules/Mypg/test2'
  import BaseControlTextArea from '@PKG_SRC/components/Basecontrol/BaseControlTextArea.vue'

  // Lấy userName từ query param ?userName=xxx
  const userNameParam = new URLSearchParams(window.location.search).get('userName') || 'UserA'
  const userName = ref(userNameParam)

  // Địa chỉ hub (khớp với server .NET)
  const hubUrl = `https://localhost:5092/chat-hub?userName=${userName.value}`

  const store = useTest2Store()

  const isChatActive = ref(false)

  // ID chat được chọn
  const selectedChatId = ref<string | null>(null)

  // Dùng computed để lấy object chat đang chọn
  const selectedChat = computed(() => {
    if (!selectedChatId.value) return null
    return store.conversations.find((item) => item.receiverId === selectedChatId.value) || null
  })

  // Lấy mảng tin nhắn của conversation đang chọn
  const currentMessages = computed(() => {
    if (!selectedChatId.value) return []
    return store.messages[selectedChatId.value] || []
  })

  const myText = ref('')
  let connection: HubConnection | null = null

  // --- AUTO-SCROLL: ref DOM cho container tin nhắn ---
  const messagesContainer = ref<HTMLElement | null>(null)
  // Hàm cuộn xuống cuối
  function scrollToBottom() {
    nextTick(() => {
      if (messagesContainer.value) {
        messagesContainer.value.scrollTo({
          top: messagesContainer.value.scrollHeight,
          behavior: 'smooth', // cuộn mượt, hoặc bỏ nếu muốn cuộn tức thời
        })
      }
    })
  }
  // Watch số lượng tin nhắn => auto scroll
  watch(
    () => currentMessages.value.length,
    () => {
      scrollToBottom()
    }
  )

  // Kết nối SignalR
  async function connectSignalR() {
    connection = new HubConnectionBuilder()
      .withUrl(hubUrl, {
        withCredentials: false,
      })
      .withAutomaticReconnect()
      .build()

    connection.on('ReceiveMessage', (sender: string, messageReceived: string) => {
      let conversationId = sender
      let who: 'mine' | 'other' = 'other'
      if (sender === userName.value) {
        who = 'mine'
        conversationId = selectedChatId.value || ''
      }
      if (!conversationId) return
      if (!store.messages[conversationId]) {
        store.messages[conversationId] = []
      }
      let existingConv = store.conversations.find((c) => c.receiverId === conversationId)
      if (!existingConv) {
        existingConv = {
          receiverId: conversationId,
          receiverName: conversationId,
          avatar: 'https://i.pravatar.cc/40?img=5',
          time: new Date().toLocaleTimeString(),
          lastMessage: messageReceived,
          unreadCount: 0,
        }
        store.conversations.push(existingConv)
      }
      existingConv.lastMessage = messageReceived
      existingConv.time = new Date().toLocaleTimeString()
      if (who === 'other') {
        if (conversationId !== selectedChatId.value) {
          existingConv.unreadCount = (existingConv.unreadCount || 0) + 1
        }
      }
      store.PushNewMessage(conversationId, {
        conversationId,
        sender: who,
        content: messageReceived,
        time: new Date().toLocaleTimeString(),
      })
    })

    try {
      await connection.start()
      console.log('Kết nối tới SignalR hub thành công!')
    } catch (err) {
      console.error('Lỗi khi kết nối SignalR:', err)
    }
  }

  async function sendCurrentMessage() {
    if (!connection || !selectedChat.value) return
    const receiverId = selectedChat.value.receiverId
    const msg = myText.value.trim()
    if (!msg) return

    try {
      await connection.invoke('SendMessage', userName.value, receiverId, msg)
      store.PushNewMessage(selectedChatId.value!, {
        conversationId: selectedChatId.value!,
        sender: 'mine',
        content: msg,
        time: new Date().toLocaleTimeString(),
      })
      myText.value = ''
    } catch (err) {
      console.error('Lỗi khi gửi tin nhắn:', err)
    }
  }

  // Khi bấm chọn 1 chat
  function handleItemClick(id: string) {
    store.GetMessageById(id)
    selectedChatId.value = id
    isChatActive.value = true
    const conv = store.conversations.find((c) => c.receiverId === id)
    if (conv) {
      conv.unreadCount = 0
    }
    scrollToBottom()
  }

  // Nút back (mobile)
  function goBackToList() {
    isChatActive.value = false
  }

  // onMounted
  onMounted(async () => {
    store.GetMessage()
    await connectSignalR()
  })
</script>
