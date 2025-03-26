<template>
  <!-- Container chính -->
  <div class="flex h-[60vh] overflow-hidden bg-gray-100">
    <!-- MAIN CHAT (luôn hiển thị, không có sidebar chọn user) -->
    <main class="flex-1 flex flex-col bg-white relative">
      <!-- Header chat -->
      <div class="flex items-center justify-between px-4 py-4 border-b border-gray-200">
        <div class="flex items-center space-x-4" v-if="selectedChat">
          <img :src="selectedChat.avatar" alt="Avatar" class="w-10 h-10 rounded-full object-cover" />
          <div>
            <h2 class="text-lg font-semibold text-gray-800">
              {{ selectedChat.receiverName }}
            </h2>
            <p class="text-sm text-gray-500">Đang trò chuyện với {{ selectedChat.receiverName }}</p>
          </div>
        </div>
        <!-- Nếu chưa có chat nào được load (dùng mặc định) -->
        <div v-else>
          <h2 class="text-lg font-semibold text-gray-800">Chưa có cuộc trò chuyện</h2>
        </div>

        <!-- Nút 3 chấm (tùy chọn) -->
        <button class="text-gray-500 hover:text-gray-700 transition-colors duration-150" v-if="selectedChat">
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
      <!-- Sử dụng ref cho container để tự động cuộn xuống dưới -->
      <div class="flex-1 overflow-y-auto p-4 space-y-4 bg-gray-50" ref="messagesContainer">
        <template v-for="(msg, index) in currentMessages" :key="index">
          <!-- Tin nhắn bên trái (other) -->
          <div v-if="msg.sender === 'other'" class="flex items-start space-x-3">
            <img :src="otherAvatar" alt="Avatar" class="w-8 h-8 rounded-full object-cover" />
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
            <img :src="myAvatar" alt="Avatar" class="w-8 h-8 rounded-full object-cover" />
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

          <!-- BaseControlTextArea có xử lý @keydown.enter.prevent -->
          <BaseControlTextArea
            v-model="myText"
            placeholder="Type a message..."
            @keydown.enter.prevent="sendCurrentMessage"
          />

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
import BaseControlTextArea from '@PKG_SRC/components/Basecontrol/BaseControlTextArea.vue'
import { useTest2Store } from '@PKG_SRC/stores/Modules/Mypg/test2'

// Định nghĩa props để nhận userName và receiverId từ bên ngoài
const props = defineProps({
  userName: {
    type: String,
    default: 'UserA',
  },
  receiverId: {
    type: String,
    default: '',
  },
})

const userName = ref(props.userName)
const selectedChatId = ref(props.receiverId || 'defaultReceiver')

// Địa chỉ SignalR hub sử dụng userName
const hubUrl = `https://localhost:5092/chat-hub?userName=${userName.value}`

const store = useTest2Store()

// Khi component được mount, nếu receiverId được truyền vào thì đảm bảo đã load tin nhắn và thông tin chat
onMounted(() => {
  store.GetMessage() // load danh sách chat mẫu
  store.GetMessageById(selectedChatId.value)
})

// Computed để lấy thông tin cuộc trò chuyện
const selectedChat = computed(() => {
  // Ưu tiên dùng thông tin đã có trong store, nếu không thì tạo dữ liệu mặc định
  return (
    store.GetConversationById(selectedChatId.value) || {
      receiverId: selectedChatId.value,
      receiverName: selectedChatId.value,
      avatar: 'https://i.pravatar.cc/40?img=2',
    }
  )
})

// Lấy mảng tin nhắn của cuộc trò chuyện được chọn
const currentMessages = computed(() => {
  return store.messages[selectedChatId.value] || []
})

const myText = ref('')
const myAvatar = 'https://i.pravatar.cc/40?img=4'
const otherAvatar = 'https://i.pravatar.cc/40?img=1'

// --- AUTO-SCROLL: ref DOM cho container tin nhắn ---
const messagesContainer = ref<HTMLElement | null>(null)
function scrollToBottom() {
  nextTick(() => {
    if (messagesContainer.value) {
      messagesContainer.value.scrollTo({
        top: messagesContainer.value.scrollHeight,
        behavior: 'smooth',
      })
    }
  })
}

// Auto-scroll mỗi khi có tin nhắn mới
watch(
  () => currentMessages.value.length,
  () => {
    scrollToBottom()
  }
)

let connection: HubConnection | null = null

async function connectSignalR() {
  connection = new HubConnectionBuilder()
    .withUrl(hubUrl, { withCredentials: false })
    .withAutomaticReconnect()
    .build()

  connection.on('ReceiveMessage', (sender: string, messageReceived: string) => {
    let conversationId = sender
    let who: 'mine' | 'other' = 'other'
    // Nếu sender trùng với userName, coi là tin của mình
    if (sender === userName.value) {
      who = 'mine'
      conversationId = selectedChatId.value
    }
    if (!conversationId) return

    if (!store.messages[conversationId]) {
      store.messages[conversationId] = []
    }
    // Nếu conversation chưa tồn tại, tạo mới với dữ liệu mặc định
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
  if (!connection) return
  const msg = myText.value.trim()
  if (!msg) return

  try {
    await connection.invoke('SendMessage', userName.value, selectedChatId.value, msg)
    // Thêm tin nhắn của mình vào store
    store.PushNewMessage(selectedChatId.value, {
      conversationId: selectedChatId.value,
      sender: 'mine',
      content: msg,
      time: new Date().toLocaleTimeString(),
    })
    myText.value = ''
  } catch (err) {
    console.error('Lỗi khi gửi tin nhắn:', err)
  }
}

onMounted(async () => {
  await connectSignalR()
})
</script>
