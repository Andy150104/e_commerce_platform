<template>
  <div class="grid grid-cols-1 gap-4 md:grid-cols-2 mb-6">
    <div>
      <DropDownSearch
        ref="parentCategoryRef"
        :xml-column="xmlColumnParentCategory"
        :maxlength="50"
        :disabled="disabledParentCategory"
        :err-msg="errMsgParentCategory"
        :placeholder="placeholderParentCategory"
        :master-name="Catogry.ParentCategory"
        :options="categoryStore.ParentCategoryList"
        @change="onChangeProvince"
        :is-category="true"
      />
    </div>
    <div>
      <DropDownSearch
        ref="childCategoryRef"
        :xml-column="xmlColumnChildCategory"
        :maxlength="50"
        :disabled="disabledChildCategory"
        :err-msg="errMsgChildCategory"
        :placeholder="placeholderChildCategory"
        :master-name="Catogry.ChildCategory"
        :options="categoryStore.ChildCategoryList"
        :is-category="true"
      />
    </div>
  </div>
</template>
<script setup lang="ts">
  import { Address, Catogry } from '@PKG_SRC/types/enums/constantFrontend'
  import DropDownSearch from '../DropDown/DropDownSearch.vue'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { useAddressStore } from '@PKG_SRC/stores/Third Modules/address/addressStore'
  import { useCatogryStore } from '@PKG_SRC/stores/Components/CategoryStore'
  import { className } from '@PKG_SRC/utils/class/className'

  const categoryStore = useCatogryStore()
  const wardRef = ref()
  const parentCategoryRef = ref()
  const childCategoryRef = ref()
  const oldCode = ref<String>('')

  const props = defineProps({
    xmlColumnParentCategory: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    errMsgParentCategory: {
      type: String,
      required: true,
      default: '',
    },
    disabledParentCategory: {
      type: Boolean,
      required: true,
    },
    maxlengthParentCategory: {
      type: Number,
      required: true,
    },
    placeholderParentCategory: {
      type: String,
      required: false,
      default: '',
    },
    optionsParentCategory: {
      type: Array as PropType<any[]>,
      required: true,
      default: [],
    },
    xmlColumnChildCategory: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    errMsgChildCategory: {
      type: String,
      required: true,
      default: '',
    },
    disabledChildCategory: {
      type: Boolean,
      required: true,
    },
    maxlengthChildCategory: {
      type: Number,
      required: true,
    },
    placeholderChildCategory: {
      type: String,
      required: false,
      default: '',
    },
    optionsChildCategory: {
      type: Array as PropType<any[]>,
      required: true,
      default: [],
    },
  })

  onMounted(() => {
    categoryStore.GetParentCategory()
  })
  const onChangeProvince = (province: string, code: string) => {
    categoryStore.parentId = code
    categoryStore.ResetCategory()
    categoryStore.GetParentCategory()
  }
  watch(
    () => categoryStore.parentId,
    (newProvince, oldProvince) => {
      if (newProvince !== oldProvince) {
        oldCode.value = categoryStore.parentId
        childCategoryRef.value.setModel('')
        categoryStore.GetParentCategory()
      }
    }
  )
</script>
