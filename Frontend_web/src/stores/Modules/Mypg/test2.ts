import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'

export const fieldsInitialize = {
  dantaiBangou: '',
  ryokouBangou: '',
  ninsyoKey: '',
  mailAddress: '',
  mailAddressConfirm: '',
  seinengappi: '',
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = fieldsInitialize

// VeeValidate
const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  // vee-validate用の初期化メソッド
  ...veeValidateStateInitialize,
}

// Định nghĩa type ProductMypp111
export type ProductMypp111 = {
  receiverId: string
  avatar: string
  time: string
  lastMessage: string
}
export type Message = {
  receiverId: string
  receiverName: string
  avatar: string
  time: string
  lastMessage: string
}
export type MessageResponse = {
  sender: string
  time: string
  content: string
}
// Dữ liệu mẫu (sample data)
const sampleData: Message[] = [
  {
    receiverId: '01',
    receiverName: 'Jay',
    avatar: 'https://i.ytimg.com/vi/lc0gis09VZs/maxresdefault.jpg',
    time: '12 mins ago',
    lastMessage: 'Hey Jan, I wanted to...',
  },
  {
    receiverId: '02',
    receiverName: 'Jaoon',
    avatar: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQF1P6sXA6sTXa7E0jjLhg-LLtKssp7pvtTrw&s',
    time: '2 mins ago',
    lastMessage: 'Hi Jan, sure I would love to. Thanks for taking the time to see my work...',
  },
  {
    receiverId: '03',
    receiverName: 'Frefi Propa',
    avatar: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQF1P6sXA6sTXa7E0jjLhg-LLtKssp7pvtTrw&s',
    time: '2 mins ago',
    lastMessage: 'Hi Jan, sure I would love to. Thanks for taking the time to see my work...',
  },
  {
    receiverId: '04',
    receiverName: 'Lok Lou',
    avatar: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQF1P6sXA6sTXa7E0jjLhg-LLtKssp7pvtTrw&s',
    time: '2 mins ago',
    lastMessage: 'Hi Jan, sure I would love to. Thanks for taking the time to see my work...',
  },
  {
    receiverId: '05',
    receiverName: 'Jaoon',
    avatar: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQF1P6sXA6sTXa7E0jjLhg-LLtKssp7pvtTrw&s',
    time: '2 mins ago',
    lastMessage: 'Hi Jan, sure I would love to. Thanks for taking the time to see my work...',
  },
  {
    receiverId: '06',
    receiverName: 'Jaoon Pef',
    avatar: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQF1P6sXA6sTXa7E0jjLhg-LLtKssp7pvtTrw&s',
    time: '2 mins ago',
    lastMessage: 'Hi Jan, sure I would love to. Thanks for taking the time to see my work...',
  },
]

const sample: MessageResponse[] = [
  {
    sender: 'other',
    time: '3:38PM',
    content: 'Hey Jan, I wanted to say i love u',
  },
  {
    sender: 'other',
    time: '3:38PM',
    content: 'Hey Maria, I wanted to reach out because we saw your work contributions and were impressed by your work. We want to invite you for a quick interview.',
  },
  {
    sender: 'other',
    time: '3:38PM',
    content: 'Great! I send over the details shortly.',
  },
  {
    sender: 'Me',
    time: '3:38PM',
    content: 'Hi Jan, sure I would love to. Thanks for taking the time to see my work.',
  },
]
const sample2: MessageResponse[] = [
    {
      sender: 'other',
      time: '3:38PM',
      content: 'Hey Jan, I wanted to say i love u2',
    },
    {
      sender: 'other',
      time: '3:38PM',
      content: '2ey Maria, I wanted to reach out because we saw your work contributions and were impressed by your work. We want to invite you for a quick interview.',
    },
    {
      sender: 'other',
      time: '3:38PM',
      content: '2Great! I send over the details shortly.',
    },
    {
      sender: 'Me',
      time: '3:38PM',
      content: '2Hi Jan, sure I would love to. Thanks for taking the time to see my work.',
    },
    {
        sender: 'Me',
        time: '3:38PM',
        content: '2Hi Jan, sure I would love to. Thanks for taking the time to see my work.',
      },
      {
        sender: 'Me',
        time: '3:38PM',
        content: 'Paragraphs are the building blocks of papers. Many students define paragraphs in terms of length: a paragraph is a group of at least five sentences, a paragraph is half a page long, etc. In reality, though, the unity and coherence of ideas among sentences is what constitutes a paragraph. A paragraph is defined as “a group of sentences or a single sentence that forms a unit” (Lunsford and Connors 116). Length and appearance do not determine whether a section in a paper is a paragraph. For instance, in some styles of writing, particularly journalistic styles, a paragraph can be just one sentence long. Ultimately, a paragraph is a sentence or group of sentences that support one main idea. In this handout, we will refer to this as the “controlling idea,” because it controls what happens in the rest of the paragraph.',
      },
      {
        sender: 'Me',
        time: '3:38PM',
        content: 'Before you can begin to determine what the composition of a particular paragraph will be, you must first decide on an argument and a working thesis statement for your paper. What is the most important idea that you are trying to convey to your reader? The information in each paragraph must be related to that idea. In other words, your paragraphs should remind your reader that there is a recurrent relationship between your thesis and the information in each paragraph. A working thesis functions like a seed from which your paper, and your ideas, will grow. The whole process is an organic one—a natural progression from a seed to a full-blown paper where there are direct, familial relationships between all of the ideas in the paper. The decision about what to put into your paragraphs begins with the germination of a seed of ideas; this “germination process” is better known as brainstorming. There are many techniques for brainstorming; whichever one you choose, this stage of paragraph development cannot be skipped. Building paragraphs can be like building a skyscraper: there must be a well-planned foundation that supports what you are building. Any cracks, inconsistencies, or other corruptions of the foundation can cause your whole paper to crumble.The decision about what to put into your paragraphs begins with the germination of a seed of ideas; this “germination process” is better known as brainstorming. There are many techniques for brainstorming; whichever one you choose, this stage of paragraph development cannot be skipped. Building paragraphs can be like building a skyscraper: there must be a well-planned foundation that supports what you are building. Any cracks, inconsistencies, or other corruptions of the foundation can cause your whole paper to crumble.The decision about what to put into your paragraphs begins with the germination of a seed of ideas; this “germination process” is better known as brainstorming. There are many techniques for brainstorming; whichever one you choose, this stage of paragraph development cannot be skipped. Building paragraphs can be like building a skyscraper: there must be a well-planned foundation that supports what you are building. Any cracks, inconsistencies, or other corruptions of the foundation can cause your whole paper to crumble.The decision about what to put into your paragraphs begins with the germination of a seed of ideas; this “germination process” is better known as brainstorming. There are many techniques for brainstorming; whichever one you choose, this stage of paragraph development cannot be skipped. Building paragraphs can be like building a skyscraper: there must be a well-planned foundation that supports what you are building. Any cracks, inconsistencies, or other corruptions of the foundation can cause your whole paper to crumble.',
      },
  ]


export type Mypg020MailState = {
  fields: typeof fields
  message: Message[]
  messageId: MessageResponse[]
}

export const useTest2Store = defineStore('Product', {
  state: (): Mypg020MailState => ({
    fields,
    message: [],
    messageId: [],
  }),
  // state

  getters: {
    fieldValues: (state) => {
      return state.fields.values
    },
    fieldErrors: (state) => {
      return state.fields.errors
    },
  },
  actions: {
    SetFields(value: any) {
      this.fields = value
    },
    ResetStore() {
      this.fields.resetForm()
    },
    GetMessage() {
      this.message = sampleData
    },
    GetMessageById(id: string){
        if (id === '01') return this.messageId = sample
        if (id === '02') return this.messageId = sample2
    }
  },
})
