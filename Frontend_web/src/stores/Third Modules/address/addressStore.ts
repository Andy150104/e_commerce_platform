import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import type { District, ProvinceResponse, Ward } from '@PKG_SRC/composables/ThirdApi/define/@types'
import { useApiAddress } from '@PKG_SRC/composables/ThirdApi/apiAddress'


export type Mypg020MailState = {
  provinceCode: String
  districtCode: String
  wardCode: String
  provinceList: ProvinceResponse[]
  DistrictList: District[]
  WardList: Ward[]
}

export const useAddressStore = defineStore('address', {
  state: (): Mypg020MailState => ({
    provinceCode: '',
    districtCode: '',
    wardCode: '',
    provinceList: [],
    DistrictList: [],
    WardList: [],
  }),
  // state

  getters: {},
  actions: {
    resetLocation() {
      this.districtCode = ''
      this.wardCode = ''
      this.DistrictList = []
      this.WardList = []
    },
    async GetProvince() {
      const apiAddress = useApiAddress()
      const res = await apiAddress.api.p.$get()
      this.provinceList = res
    },
    async GetDistrict() {
      const apiAddress = useApiAddress()
      const res = await apiAddress.api.p._code(Number(this.provinceCode)).$get({
        query: {
          depth: 2,
        },
      })
      this.DistrictList = res.districts ?? []
      console.log('a')
      return
    },
    async GetWard() {
      const apiAddress = useApiAddress()
      const res = await apiAddress.api.d._code(Number(this.districtCode)).$get({
        query: {
          depth: 2,
        },
      })
      this.WardList = res.wards ?? []
      console.log('b')
      return
    },
  },
})
