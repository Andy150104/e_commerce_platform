<template>
  <div>
    <div v-if="addressStore.provinceList.length > 0">
      <DropDownSearch
        :xml-column="xmlColumnProvince"
        :maxlength="50"
        :disabled="disabledProvince"
        :err-msg="errMsgProvince"
        :placeholder="placeholderProvince"
        :master-name="Address.Province"
        :options="addressStore.provinceList"
        @change="onChangeProvince"
      />
    </div>
    <div>
      <DropDownSearch
        ref="districtRef"
        :xml-column="xmlColumnDistrict"
        :maxlength="50"
        :disabled="disabledDistrict"
        :err-msg="errMsgDistrict"
        :placeholder="placeholderDistrict"
        :master-name="Address.District"
        :options="addressStore.DistrictList"
      />
    </div>
    <div>
      <DropDownSearch
        ref="wardRef"
        :xml-column="xmlColumnWard"
        :maxlength="50"
        :disabled="disabledWard"
        :err-msg="errMsgWard"
        :placeholder="placeholderWard"
        :master-name="Address.Ward"
        :options="addressStore.WardList"
      />
    </div>
  </div>
</template>
<script setup lang="ts">
  import { Address } from '@PKG_SRC/types/enums/constantFrontend'
  import DropDownSearch from '../DropDown/DropDownSearch.vue'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { useAddressStore } from '@PKG_SRC/stores/Third Modules/address/addressStore'

  const addressStore = useAddressStore()
  const wardRef = ref()
  const districtRef = ref()
  const oldCode = ref<String>('')

  const props = defineProps({
    xmlColumnProvince: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    errMsgProvince: {
      type: String,
      required: true,
      default: '',
    },
    disabledProvince: {
      type: Boolean,
      required: true,
    },
    maxlengthProvince: {
      type: Number,
      required: true,
    },
    placeholderProvince: {
      type: String,
      required: false,
      default: '',
    },
    optionsProvince: {
      type: Array as PropType<any[]>,
      required: true,
      default: [],
    },
    xmlColumnDistrict: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    errMsgDistrict: {
      type: String,
      required: true,
      default: '',
    },
    disabledDistrict: {
      type: Boolean,
      required: true,
    },
    maxlengthDistrict: {
      type: Number,
      required: true,
    },
    placeholderDistrict: {
      type: String,
      required: false,
      default: '',
    },
    optionsDistrict: {
      type: Array as PropType<any[]>,
      required: true,
      default: [],
    },
    xmlColumnWard: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    errMsgWard: {
      type: String,
      required: true,
      default: '',
    },
    disabledWard: {
      type: Boolean,
      required: true,
    },
    maxlengthWard: {
      type: Number,
      required: true,
    },
    placeholderWard: {
      type: String,
      required: false,
      default: '',
    },
    optionsWard: {
      type: Array as PropType<any[]>,
      required: true,
      default: [],
    },
  })

  onMounted(() => {
    addressStore.GetProvince()
  })
  const onChangeProvince = (province: string, code: string) => {
    addressStore.provinceCode = code
    addressStore.resetLocation() // Reset District và Ward khi chọn Province khác
    addressStore.GetDistrict() // Gọi API lấy danh sách District mới
  }
  watch(
    () => addressStore.provinceCode, // Theo dõi giá trị provinceCode
    (newProvince, oldProvince) => {
      if (newProvince !== oldProvince) {
        console.log('vao watch', oldCode.value, 'province', addressStore.provinceCode)
        oldCode.value = addressStore.provinceCode
        wardRef.value.setModel('')
        districtRef.value.setModel('')
      }
    }
  )
</script>
