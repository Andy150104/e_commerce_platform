<template>
  <!-- Container chính -->
  <div class="flex h-screen overflow-hidden bg-gray-100">

    <!-- SIDEBAR (Nhập tên người nhận) -->
    <aside
      class="w-full md:w-1/4 flex flex-col bg-white border-r border-gray-200 shadow-sm"
      :class="[ isChatActive ? 'hidden' : 'flex', 'md:flex' ]"
    >
      <!-- Header sidebar -->
      <div class="px-4 py-4 border-b border-gray-200 flex items-center justify-between">
        <h1 class="text-xl font-semibold text-gray-800">Messages</h1>
      </div>

      <!-- Nhập tên người nhận -->
      <div class="p-4 border-b border-gray-200">
        <div class="flex flex-col space-y-2">
          <label class="text-sm font-medium text-gray-600">
            Nhập tên người nhận:
          </label>
          <input
            type="text"
            v-model="enteredReceiver"
            placeholder="Ví dụ: UserB"
            class="w-full px-4 py-2 rounded border border-gray-300 bg-white
                   focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <button
            class="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded
                   focus:outline-none transition-colors duration-150 self-start"
            @click="chooseReceiver"
          >
            Chọn
          </button>
        </div>
      </div>

      <!-- (Tuỳ ý) Thêm nội dung khác bên dưới nếu muốn -->
      <div class="flex-1 overflow-y-auto p-4 text-gray-500">
        <!-- Ở đây có thể để trống hoặc hướng dẫn người dùng -->
        <p>Hãy nhập tên người nhận (đã kết nối trên SignalR) để chat.</p>
      </div>
    </aside>

    <!-- MAIN CHAT -->
    <main
      class="flex-1 flex flex-col bg-white relative"
      :class="[ isChatActive ? 'flex' : 'hidden', 'md:flex' ]"
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

        <!-- Nếu đã chọn 1 chat (tức đã có receiverName) -->
        <div class="flex items-center space-x-4" v-if="selectedChat">
          <!-- Ảnh demo -->
          <img
            :src="selectedChat.avatar"
            alt="Avatar"
            class="w-10 h-10 rounded-full object-cover"
          />
          <div>
            <h2 class="text-lg font-semibold text-gray-800">
              {{ selectedChat.receiverName }}
            </h2>
            <p class="text-sm text-gray-500">Đang trò chuyện với {{ selectedChat.receiverName }}</p>
          </div>
        </div>

        <!-- Nếu chưa chọn chat nào -->
        <div v-else>
          <h2 class="text-lg font-semibold text-gray-800">
            Chưa chọn người nhận
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
        <template v-for="(msg, index) in messages" :key="index">
          <!-- Tin nhắn bên trái (other) -->
          <div v-if="msg.sender === 'other'" class="flex items-start space-x-3">
            <img
              :src="otherAvatar"
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
              :src="myAvatar"
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
          <button class="text-gray-400 hover:text-gray-600 transition-colors duration-150">
            <!-- Icon tuỳ ý (đính kèm, emoji, v.v.) -->
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none"
                 viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M15.172 7l-6.586 6.586a2 2 0 102.828 2.828L18 9.828V7h-2.828z" />
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M5 19l-2 2m14-2l2 2" />
            </svg>
          </button>

          <!-- BaseControlTextArea (hoặc input thường) -->
          <BaseControlTextArea
            v-model="myText"
            placeholder="Type a message..."
          />
          <button
            class="bg-blue-500 hover:bg-blue-600 text-white p-3 rounded-full
                   focus:outline-none transition-colors duration-150"
            @click="sendCurrentMessage"
            :disabled="!selectedChat"
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
/* ------------ IMPORT & SETUP ------------- */
import { ref, onMounted } from 'vue'
import BaseControlTextArea from '@PKG_SRC/components/Basecontrol/BaseControlTextArea.vue'
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr'

/* ----- LẤY userName TỪ QUERY PARAM ----- */
const userNameParam = new URLSearchParams(window.location.search).get('userName') || 'UserA'
const userName = ref(userNameParam)

/* ----- TẠO URL HUB (chứa userName) ----- */
const hubUrl = `https://localhost:5092/chat-hub?userName=${userName.value}`

/* ----- LAYOUT & STATE ----- */
// Sidebar: user sẽ nhập tên người nhận
const enteredReceiver = ref('')

// Xác định đang ở chế độ mobile hay desktop
const isChatActive = ref(false)

// Thông tin "chat" đã chọn (ở đây ta chỉ cần receiverName, avatar)
const selectedChat = ref<null | { receiverName: string; avatar: string }>(null)

// Danh sách tin nhắn trong khung chat
const messages = ref<Array<{ sender: 'mine' | 'other'; content: string; time: string }>>([])

// Nội dung tin nhắn đang soạn
const myText = ref('')

/* ----- AVATAR MẪU (cho UI) ----- */
const myAvatar = 'https://i.pravatar.cc/40?img=4'
const otherAvatar = 'https://i.pravatar.cc/40?img=1'

/* ----- KẾT NỐI SIGNALR ----- */
let connection: HubConnection | null = null

async function connectSignalR() {
  connection = new HubConnectionBuilder()
    .withUrl(hubUrl, {
      withCredentials: false
    })
    .withAutomaticReconnect()
    .build()

  // Lắng nghe sự kiện ReceiveMessage(sender, message)
  connection.on('ReceiveMessage', (sender: string, messageReceived: string) => {
    // Xác định tin nhắn là của mình hay người khác
    const who = sender === userName.value ? 'mine' : 'other'
    // Đẩy vào mảng messages
    messages.value.push({
      sender: who,
      content: messageReceived,
      time: new Date().toLocaleTimeString()
    })
  })

  try {
    await connection.start()
    console.log('Kết nối tới SignalR hub thành công!')
  } catch (err) {
    console.error('Lỗi khi kết nối SignalR:', err)
  }
}

/* ----- XỬ LÝ CHỌN NGƯỜI NHẬN ----- */
function chooseReceiver() {
  const r = enteredReceiver.value.trim()
  if (!r) return

  // Giả sử avatar fix cứng hoặc tùy
  selectedChat.value = {
    receiverName: r,
    avatar: 'https://i.pravatar.cc/40?img=2'
  }

  // Chuyển sang khung chat (trên mobile)
  isChatActive.value = true
}

/* ----- GỬI TIN NHẮN ----- */
async function sendCurrentMessage() {
  if (!connection || !selectedChat.value) return

  const msg = myText.value.trim()
  if (!msg) return

  // Gửi lên server => SendMessage(sender, receiver, msg)
  try {
    await connection.invoke('SendMessage', userName.value, selectedChat.value.receiverName, msg)
    // Tự thêm vào UI (của mình)
    messages.value.push({
      sender: 'mine',
      content: msg,
      time: new Date().toLocaleTimeString()
    })
    myText.value = ''
  } catch (err) {
    console.error('Lỗi khi gửi tin nhắn:', err)
  }
}

/* ----- NÚT BACK (MOBILE) ----- */
function goBackToList() {
  isChatActive.value = false
}

/* ----- onMounted ----- */
onMounted(async () => {
  await connectSignalR()
})
</script>

<style scoped>
/* Tuỳ chỉnh thêm nếu cần */
</style>
