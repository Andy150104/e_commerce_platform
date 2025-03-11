<template>
  <div class="mb-3">
    <div class="reactive">
      <Field
        :id="fieldId"
        v-model="model"
        :type="inputType"
        :class="classField"
        :data-enter-move="move"
        :name="xmlColumn.id"
        pattern="^[0-9A-Z]+"
        :rules="fieldRules"
        onfocus="select()"
        :disabled="disabled"
        :readonly="readonly"
        :placeholder="props.placeholder"
        @blur="onBlur"
        @change="onChange"
        @input="onInput"
      />
      <button v-if="props.isPasswordVisibility" type="button" class="toggle-password" @click="togglePassword">
        <!-- Sử dụng icon tùy chỉnh, thay đổi theo trạng thái -->
        <svg
          v-if="showPassword"
          class="w-6 h-6 text-gray-800 dark:text-white"
          aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          fill="none"
          viewBox="0 0 24 24"
        >
          <path
            stroke="currentColor"
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M3.933 13.909A4.357 4.357 0 0 1 3 12c0-1 4-6 9-6m7.6 3.8A5.068 5.068 0 0 1 21 12c0 1-3 6-9 6-.314 0-.62-.014-.918-.04M5 19 19 5m-4 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
          />
        </svg>
        <svg
          v-else
          class="w-6 h-6 text-gray-800 dark:text-white"
          aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          fill="none"
          viewBox="0 0 24 24"
        >
          <path stroke="currentColor" stroke-width="2" d="M21 12c0 1.2-4.03 6-9 6s-9-4.8-9-6c0-1.2 4.03-6 9-6s9 4.8 9 6Z" />
          <path stroke="currentColor" stroke-width="2" d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
        </svg>
      </button>
      <LabelItem :xml-column="xmlColumn" :class-label="classLabelField" />
    </div>
    <UAlert
      v-if="errMessage"
      icon="i-heroicons-command-line"
      color="rose"
      padding="p-1"
      variant="outline"
      :title="errMsg"
      :close-button="{ icon: 'i-heroicons-x-mark-20-solid', color: 'gray', variant: 'link' }"
      :ui="{ padding: 'p-1 mt-3 mb-3' }"
      @close="onClose"
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
    isPasswordVisibility: {
      type: Boolean,
      required: false,
      default: false,
    },
    isNumberic: {
      type: Boolean,
      required: false,
      default: false,
    },
    isNotPhoneNumber: {
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
  const showPassword = ref(false)
  // Hàm chuyển đổi trạng thái
  const togglePassword = () => {
    showPassword.value = !showPassword.value
  }

  const inputType = computed(() => {
    return props.type === 'password' ? (showPassword.value ? 'text' : 'password') : props.type
  })

  const onClose = () => {
    errMessage.value = ''
  }
  // Blurイベント
  const onBlur = () => {
    errMessage.value = props.errMsg
    if (model.value === null) {
      model.value = ''
    }
    if (props.isNumberic) {
      model.value = model.value.replace(/[^0-9]/g, '')
    }
    if (props.isNotPhoneNumber && model.value.startsWith('0')) {
      model.value = model.value.replace(/^0/, '')
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
    return errMessage.value ? className.LABEL_FLOAT_ERROR : className.LABEL_FLOAT
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
<style scoped>
  input[type='password']::-ms-reveal,
  input[type='password']::-ms-clear {
    display: none;
  }

  /* Với trình duyệt dựa trên Webkit (nếu có) */
  input[type='password']::-webkit-textfield-decoration-container {
    display: none;
  }

  .reactive {
    position: relative;
  }

  /* Class cho nút toggle mật khẩu tùy chỉnh */
  .toggle-password {
    position: absolute;
    right: 10px;
    top: 50%;
    transform: translateY(-50%);
    background: transparent;
    border: none;
    cursor: pointer;
    /* Bạn có thể tuỳ chỉnh thêm style cho nút ở đây */
  }
  input:-webkit-autofill {
    background-color: transparent !important; /* Hoặc màu bạn muốn */
    color: inherit !important; /* Giữ màu chữ theo thiết kế */
    box-shadow: 0 0 0px 1000px white inset !important; /* Màu nền field */
    -webkit-text-fill-color: black !important; /* Màu chữ */
    transition: background-color 5000s ease-in-out 0s; /* Để tránh đổi màu */
  }

  input:-webkit-autofill:hover,
  input:-webkit-autofill:focus {
    background-color: transparent !important;
    box-shadow: 0 0 0px 1000px white inset !important;
    -webkit-text-fill-color: black !important;
  }
</style>
