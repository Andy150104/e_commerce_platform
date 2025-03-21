<template>
  <div class="relative">
    <LabelItem :xml-column="xmlColumn" />
    <div>
      <Field
        :id="fieldId"
        v-model="model"
        :type="type"
        :class="classField"
        :data-enter-move="move"
        :name="xmlColumn.id"
        :rules="fieldRules"
        onfocus="select()"
        :disabled="disabled"
        :readonly="readonly"
        @focus="isDropdownOpen = true"
        :placeholder="props.placeholder"
        @blur="onBlur"
        autocomplete="off"
        aria-autocomplete="none"
        @change="onChange"
        @input="onInput"
      />
    </div>
    <ul
      v-if="isDropdownOpen && filteredOptions.length"
      class="absolute w-full bg-white text-black border border-gray-300 rounded-lg shadow-lg z-10 max-h-48 overflow-y-auto list-none dark:text-white dark:bg-slate-900"
    >
      <div v-if="isCategory">
        <li
          v-for="option in filteredOptions"
          :key="option.value"
          class="p-2 hover:bg-blue-100 cursor-pointer"
          @click="selectCategory(option.name, option.code)"
        >
          {{ option.name }}
        </li>
      </div>
      <div v-else>
        <li
          v-for="option in filteredOptions"
          :key="option.value"
          class="p-2 hover:bg-blue-100 cursor-pointer"
          @click="selectProvince(option.name, option.code)"
        >
          {{ option.name }}
        </li>
      </div>
    </ul>
  </div>
</template>

<script setup lang="ts">
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { className } from '@PKG_SRC/utils/class/className'
  import { Field } from 'vee-validate'
  import { uuid } from 'vue-uuid'
  import { Address, Catogry } from '@PKG_SRC/types/enums/constantFrontend'
  import LabelItem from '../Basecontrol/LabelItem.vue'
  import { useAddressStore } from '@PKG_SRC/stores/Third Modules/address/addressStore'
  import { useCatogryStore } from '@PKG_SRC/stores/Components/CategoryStore'

  const isDropdownOpen = ref(false)

  const props = defineProps({
    textModel: {
      type: String,
      required: false,
      default: '',
    },
    xmlColumn: {
      type: Object as PropType<xmlColumn>,
      required: true,
    },
    masterName: {
      type: String,
      required: true,
    },
    errMsg: {
      type: String,
      required: true,
      default: '',
    },
    move: {
      type: Boolean,
      required: false,
      default: true,
    },
    disabled: {
      type: Boolean,
      required: true,
    },
    maxlength: {
      type: Number,
      required: true,
    },
    placeholder: {
      type: String,
      required: false,
      default: '',
    },
    classField: {
      type: String,
      default: className.INPUT,
    },
    isShowError: {
      type: Boolean,
      required: false,
      default: true,
    },
    readonly: {
      type: Boolean,
      required: false,
      default: false,
    },
    type: {
      type: String,
      required: false,
      default: 'text',
    },
    options: {
      type: Object as PropType<any[]>,
      required: true,
      default: [],
    },
    isCategory: {
      type: Boolean,
      required: false,
      default: false,
    },
  })
  interface Emits {
    (e: 'on-blur'): void
    (e: 'on-change'): void
    (e: 'on-input'): void
  }

  const emit = defineEmits<Emits>()

  const fieldId = uuid.v4()
  const fieldRules = computed(() => props.xmlColumn.rules)

  const model = ref(props.textModel)
  const errMessage = ref(props.errMsg)
  const addressStore = useAddressStore()
  const categoryStore = useCatogryStore()

  const filteredOptions = computed(() => {
    if (!model.value) return props.options
    return props.options.filter((option) => option.name.toLowerCase().includes(model.value.toLowerCase()))
  })

  const selectProvince = (province: string, code: string) => {
    setModel(province)
    if (props.masterName === Address.Province) {
      addressStore.provinceCode = code
      addressStore.GetDistrict()
    }
    if (props.masterName === Address.District) {
      addressStore.districtCode = code
      addressStore.GetWard()
    }
    if (props.masterName === Address.Ward) {
      addressStore.wardCode = code
    }
    isDropdownOpen.value = false
  }
  const selectCategory = (category: string, code: string) => {
    setModel(category)
    if (props.masterName === Catogry.ParentCategory) {
      categoryStore.parentId = code
      categoryStore.GetChildCategory()
    }
    if (props.masterName === Catogry.ChildCategory) {
      categoryStore.childId = code
    }
    isDropdownOpen.value = false
  }
  const onBlur = () => {
    errMessage.value = props.errMsg
    if (model.value === null) {
      model.value = ''
    }
    model.value = model.value.slice(0, props.maxlength)
    nextTick(() => {
      emit('on-blur')
    })
  }
  const onInput = () => {
    nextTick(() => {
      emit('on-input')
    })
  }
  const setModel = (value: string) => {
    model.value = value
  }
  const classField = computed(() => {
    return errMessage.value ? className.INPUT_ERROR : className.INPUT
  })
  const onChange = () => {
    nextTick(() => {
      emit('on-change')
    })
  }

  window.addEventListener('click', (e) => {
    if (e.target && !(e.target as HTMLElement).closest('.relative')) {
      isDropdownOpen.value = false
    }
  })
  onUnmounted(() => {
    window.removeEventListener('click', (e) => {
      if (e.target && !(e.target as HTMLElement).closest('.relative')) {
        isDropdownOpen.value = false
      }
    })
  })
  onMounted(() => {})
  defineExpose({
    setModel,
  })
</script>

<style scoped>
  /* Để tạo hiệu ứng đẹp hơn */
</style>
