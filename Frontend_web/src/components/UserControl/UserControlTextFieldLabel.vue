<template>
  <div>
    <LabelItem :xml-column="xmlColumn" />
    <BaseControlTextField
      :xml-column="xmlColumn"
      :type="type"
      :maxlength="maxlength"
      :disabled="disabled"
      :err-msg="errMsg"
      :placeholder="placeholder"
    />
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
  import LabelItem from '../Basecontrol/LabelItem.vue'
  import BaseControlTextField from '../Basecontrol/BaseControlTextField.vue'
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
      default: className.INPUT_LABEL_FLOAT,
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

  const onClose = () => {
    errMessage.value = ''
  }
  // Blurイベント
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
  const classField = computed(() => {
    return errMessage.value ? className.INPUT_LABEL_FLOAT_ERROR : className.INPUT_LABEL_FLOAT
  })

  const classLabelField = computed(() => {
    return errMessage.value ? className.LABEL_DEFAULT_ERROR : className.LABEL_DEFAULT
  })

  const onChange = () => {
    nextTick(() => {
      emit('on-change')
    })
  }

  const onInput = () => {
    nextTick(() => {
      emit('on-input')
    })
  }
  /**
   * 親からFocusを当てる
   */
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
<style scoped></style>
