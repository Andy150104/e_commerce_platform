import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import type { AbstractApiResponseOfString, MPSSelectAccessoriesEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useCatogryStore } from '@PKG_SRC/stores/Components/CategoryStore'
import { useLoadingStore } from '../../usercontrol/loadingStore'
export type FormSchema = Record<string, string>

export const fieldsInitialize: FormSchema = {
  quantity: '1',
  description: '',
  name: '',
  price: '',
  discount: '',
  shortDescription: '',
  parentCategory: '',
  childCategory: '',
  quantityUpdate: '1',
  descriptionUpdate: '',
  nameUpdate: '',
  priceUpdate: '',
  discountUpdate: '',
  shortDescriptionUpdate: '',
  parentCategoryUpdate: '',
  childCategoryUpdate: '',
}

const errorFieldsInitialize: FormSchema = { ...fieldsInitialize }

// VeeValidate fields
const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type QueueItem = {
  queueId: string
  userFullNameQueue: string
  userImage: string
  descriptionQueue: string
  userGender: 'male' | 'female' | 'other'
  userPhone: string
  userBirthday: string
}

export const queueListDetail: QueueItem[] = [
  {
    queueId: '2dglnasgaw213213123e121e2',
    userFullNameQueue: 'Lavie',
    userImage: 'https://example.com/image1.jpg',
    descriptionQueue: 'Tôi muốn đổi 3 blindbox đổi 1 blindbox',
    userGender: 'male',
    userPhone: '0199312313',
    userBirthday: '2003-11-12',
  },
  {
    queueId: '3hjsdf87sdasdas987d9a8sd7',
    userFullNameQueue: 'Alice',
    userImage: 'https://example.com/image2.jpg',
    descriptionQueue: 'Tôi muốn đổi 2 blindbox lấy 1 hộp quà đặc biệt',
    userGender: 'female',
    userPhone: '0987654321',
    userBirthday: '1999-05-25',
  },
  {
    queueId: '4hf8e3ufasdb9a8sd7a98sd',
    userFullNameQueue: 'John Doe',
    userImage: 'https://example.com/image3.jpg',
    descriptionQueue: 'Có ai muốn đổi 5 blindbox không?',
    userGender: 'male',
    userPhone: '0123456789',
    userBirthday: '1995-07-15',
  },
  {
    queueId: '5gfsa98sd7f9as8d7fa9s8df7',
    userFullNameQueue: 'Emma Watson',
    userImage: 'https://example.com/image4.jpg',
    descriptionQueue: 'Mình cần đổi 1 blindbox màu đỏ',
    userGender: 'female',
    userPhone: '0934567890',
    userBirthday: '2001-02-10',
  },
  {
    queueId: '6hgj23hfbsad98as7d9a87sd9',
    userFullNameQueue: 'Chris Evans',
    userImage: 'https://example.com/image5.jpg',
    descriptionQueue: 'Tôi muốn đổi 1 blindbox lấy một phụ kiện độc quyền',
    userGender: 'male',
    userPhone: '0976543210',
    userBirthday: '1990-08-30',
  },
]

export type MPSProductState = {
  fields: typeof fields
  queueListDetail: QueueItem[]
}

export const useQueueStore = defineStore('queueStore', {
  state: (): MPSProductState => ({
    fields,
    queueListDetail: [],
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
    async GetQueue(search: string) {
      this.queueListDetail = queueListDetail
      return true
    },
  },
})
