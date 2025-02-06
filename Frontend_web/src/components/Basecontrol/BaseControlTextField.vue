<template>
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
      :placeholder="props.placeholder"
      @blur="onBlur"
      @change="onChange"
      @input="onInput"
    />
  </div>
  <div v-if="errMessage">
    <!-- <span class="text-red-500 text-sm">{{ errMsg }}</span> -->
    <UAlert
      icon="i-heroicons-command-line"
      color="rose"
      padding="p-1"
      variant="outline"
      :title="errMsg"
      :close-button="{ icon: 'i-heroicons-x-mark-20-solid', color: 'gray', variant: 'link' }"
      :ui="{ padding: 'p-1 mt-3 mb-3', wrapper: 'w-full relative overflow-hidden' }"
      @close="onClose"
    />
  </div>
</template>
<script setup lang="ts">
  import { computed, onMounted, type PropType } from 'vue'
  import { ref, watch, nextTick } from 'vue'
  import { uuid } from 'vue-uuid'
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { Field } from 'vee-validate'
  import { _padding } from '#tailwind-config/theme'
  import { className } from '@PKG_SRC/utils/class/className'
  const props = defineProps({
    textModel: {
      type: String,
      required: true,
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
    }
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
    return errMessage.value ? className.INPUT_ERROR : className.INPUT
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
