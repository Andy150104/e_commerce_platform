<template>
  <div class="md:flex md:items-center md:space-x-4 space-y-4 md:space-y-0 w-full">
    <div class="relative w-full">
      <div class="absolute left-3 top-4">
        <svg
          class="w-4 h-4 text-gray-500 dark:text-gray-400"
          aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg"
          fill="currentColor"
          viewBox="0 0 20 20"
        >
          <path
            d="M20 4a2 2 0 0 0-2-2h-2V1a1 1 0 0 0-2 0v1h-3V1a1 1 0 0 0-2 0v1H6V1a1 1 0 0 0-2 0v1H2a2 2 0 0 0-2 2v2h20V4ZM0 18a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V8H0v10Zm5-8h10a1 1 0 0 1 0 2H5a1 1 0 0 1 0-2Z"
          />
        </svg>
      </div>
      <Field
        :id="fieldId"
        v-model="dateModel"
        type="text"
        :class="classField"
        :data-enter-move="move"
        :name="xmlColumn.id"
        :rules="dateRules"
        onfocus="select()"
        datepicker
        datepicker-buttons
        datepicker-format="dd-mm-yyyy"
        datepicker-autoselect-today
        :disabled="disabled"
        :readonly="readonly"
        :placeholder="props.placeholder"
        autocomplete="off"
        @blur="onBlur"
      />
      <div v-if="errMessage">
    <UAlert
      icon="i-heroicons-command-line"
      color="rose"
      padding="p-1"
      variant="outline"
      :title="errMsg"
      :close-button="{ icon: 'i-heroicons-x-mark-20-solid', color: 'gray', variant: 'link' }"
      :ui="{ padding: 'p-1 mt-3 mb-3'}"
      @close="onClose"
    />
  </div>
    </div>
  </div>
</template>
<script setup lang="ts">
  import { computed, type PropType } from 'vue'
  import { ref, watch, nextTick } from 'vue'
  import { uuid } from 'vue-uuid'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { Field } from 'vee-validate'
  import { _padding } from '#tailwind-config/theme'
  import { className } from '@PKG_SRC/utils/class/className'
  import LabelItem from './LabelItem.vue'
  const props = defineProps({
    xmlColumn: {
      type: Object as PropType<xmlColumn>,
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
    dateModel: {
      type: String,
      required: true,
      default: '',
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
      default: className.DATE_TIME_PICKER_TODAY,
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
  })
  interface Emits {
    (e: 'on-blur'): void
    (e: 'update:dateModel', value: any): void
  }

  const emit = defineEmits<Emits>()

  const fieldId = uuid.v4()
  const dateRules = ref('date_format:' + fieldId + '|' + props.xmlColumn.rules)
  const errMessage = ref(props.errMsg)

  const onClose = () => {
    errMessage.value = ''
  }
  const dateModel = ref(props.dateModel)
  // Blurイベント
  const onBlur = () => {
    const inputElement = document.getElementById(fieldId) as HTMLInputElement
    dateModel.value = dateModel.value.replaceAll(dateModel.value, inputElement.value)
    errMessage.value = props.errMsg
    if (dateModel.value === null) {
      dateModel.value = ''
    }
    dateModel.value = dateModel.value.slice(0, props.maxlength)
    nextTick(() => {
      emit('on-blur')
    })
  }
  
  const classField = computed(() => {
    return errMessage.value ? className.DATE_TIME_PICKER_TODAY : className.DATE_TIME_PICKER_TODAY
  })

  const onFocus = () => {
    const id = document.getElementById(fieldId) as HTMLInputElement
    id.focus()
    id.select()
  }
  watch(
    () => props.errMsg,
    (err: string) => {
      errMessage.value = err
    }
  )
  //   watch(
  //     () => props.errMsg,
  //     (msg: string) => {
  //       messageShowHide(fieldId, msg)
  //     }
  //   )
  // 親コンポーネントから参照できるメソッド
  defineExpose({
    onFocus,
  })
</script>
<style scoped>
  .width-date {
    width: 100% !important;
  }
</style>
