import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import { useUserStore } from '@PKG_SRC/stores/master/userStore'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'

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
  sender: string
  content: string
  time: string
}

export const useTest2Store = defineStore('test2', {
  state: () => ({
    conversations: [] as ConversationItem[],
    messages: {} as Record<string, ChatMessage[]>,
  }),

  actions: {
    async GetMessage() {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.GetMessage.$get()
      loadingStore.LoadingChange(false)
      if (!res.success) {
        return false
      }
      if (res.response) {
        this.conversations = res.response.map((item) => ({
          receiverId: item.receiverId || '',
          receiverName: item.receiverName || '',
          avatar: item.avatar || '',
          time: item.time || '',
          lastMessage: item.lastMessage || '',
          unreadCount: 0,
        }))
      }
      return true
    },
    GetConversationById(id: string): ConversationItem | undefined {
      return this.conversations.find((c) => c.receiverId === id)
    },
    async GetMessageById(id: string) {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.GetMessageWUserName.$get({
        query:{
          UserName: id
        }
      })
      loadingStore.LoadingChange(false)
      if (!res.success) {
        formMessage.SetFormMessageNotApiRes('E00001', true, res.message ?? '')
        return false
      }
      if (res.response) {
        this.messages[id] = res.response.map(item => ({
          conversationId: item.conversationId ?? '',
          sender: item.sender ?? 'mine',
          content: item.content ?? '',
          time: item.createdAt ?? ''
        }))
      }
      return true
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
    },
  },
})
