import { defineStore } from 'pinia'

export interface ConversationItem {
  receiverId: string
  receiverName: string
  avatar: string
  time: string
  lastMessage: string
  unreadCount: number
}

export interface ChatMessage {
  conversationId: string
  sender: 'mine' | 'other'
  content: string
  time: string
}

export const useTest2Store = defineStore('test2', {
  state: () => ({
    conversations: [] as ConversationItem[],
    messages: {} as Record<string, ChatMessage[]>
  }),

  actions: {
    GetMessage() {
      this.conversations = [
        {
          receiverId: 'User01',
          receiverName: 'Jay',
          avatar: 'https://i.pravatar.cc/40?img=1',
          time: '12:30',
          lastMessage: 'Hello from Jay',
          unreadCount: 0
        },
        {
          receiverId: 'User02',
          receiverName: 'Playmaka',
          avatar: 'https://i.pravatar.cc/40?img=1',
          time: '12:30',
          lastMessage: 'Hello from Jay',
          unreadCount: 0
        },
        {
          receiverId: 'User03',
          receiverName: 'Maria',
          avatar: 'https://i.pravatar.cc/40?img=2',
          time: '12:32',
          lastMessage: 'Hello from Maria',
          unreadCount: 0
        },
        {
          receiverId: 'User04',
          receiverName: 'UserC',
          avatar: 'https://i.pravatar.cc/40?img=3',
          time: '12:33',
          lastMessage: 'Hi there...',
          unreadCount: 0
        }
      ]
    },

    GetMessageById(id: string) {
      if (!this.messages[id]) {
        this.messages[id] = [
          {
            conversationId: id,
            sender: 'other',
            content: `Hello from ${id}`,
            time: '12:30'
          },
          {
            conversationId: id,
            sender: 'mine',
            content: `Hi ${id}, how are you?`,
            time: '12:31'
          }
        ]
      }
    },

    // Hàm push tin nhắn mới
    PushNewMessage(conversationId: string, newMsg: ChatMessage) {
      if (!this.messages[conversationId]) {
        this.messages[conversationId] = []
      }
      this.messages[conversationId].push(newMsg)

      const conv = this.conversations.find((c) => c.receiverId === conversationId)
      if (conv) {
        conv.lastMessage = newMsg.content
        conv.time = newMsg.time
      }
    }
  }
})
