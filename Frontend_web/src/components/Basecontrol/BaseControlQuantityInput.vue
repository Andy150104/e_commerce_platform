<template>
  <label v-if="isShowLabel" :for="fieldId" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Choose quantity:</label>
  <div class="relative flex items-center max-w-[11rem]">
    <button
      type="button"
      id="decrement-button"
      :data-input-counter-decrement="fieldId"
      @click="onClickQuantity"
      class="bg-gray-100 dark:bg-gray-700 dark:hover:bg-gray-600 dark:border-gray-600 hover:bg-gray-200 border border-gray-300 rounded-s-lg p-3 h-11 focus:ring-gray-100 dark:focus:ring-gray-700 focus:ring-2 focus:outline-none"
    >
      <svg class="w-3 h-3 text-gray-900 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 18 2">
        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M1 1h16" />
      </svg>
    </button>
    <Field
      :id="fieldId"
      v-model="model"
      :type="type"
      :class="'bg-gray-50 border-x-0 border-gray-300 h-11 font-medium text-center text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full pb-6 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'"
      :name="xmlColumn.id"
      :rules="fieldRules"
      onfocus="select()"
      :disabled="disabled"
      :readonly="readonly"
      :placeholder="props.placeholder"
      data-input-counter
      data-input-counter-min="0"
      data-input-counter-max="99"
      aria-describedby="helper-text-explanation"
      @blur="onBlur"
      @change="onChange"
      @input="onInput"
    />
    <div
      class="absolute bottom-1 start-1/2 -translate-x-1/2 rtl:translate-x-1/2 flex items-center text-xs text-gray-400 space-x-1 rtl:space-x-reverse"
    >
      <svg class="w-2.5 h-2.5 text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 20 20">
        <path
          stroke="currentColor"
          stroke-linecap="round"
          stroke-linejoin="round"
          stroke-width="2"
          d="M3 8v10a1 1 0 0 0 1 1h4v-5a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v5h4a1 1 0 0 0 1-1V8M1 10l9-9 9 9"
        />
      </svg>
      <span>Product</span>
    </div>
    <button
      type="button"
      id="increment-button"
      :data-input-counter-increment="fieldId"
      @click="onClickQuantity"
      class="bg-gray-100 dark:bg-gray-700 dark:hover:bg-gray-600 dark:border-gray-600 hover:bg-gray-200 border border-gray-300 rounded-e-lg p-3 h-11 focus:ring-gray-100 dark:focus:ring-gray-700 focus:ring-2 focus:outline-none"
    >
      <svg class="w-3 h-3 text-gray-900 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 18 18">
        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 1v16M1 9h16" />
      </svg>
    </button>
  </div>
</template>
<script setup lang="ts">
  import type { xmlColumn } from '@PKG_SRC/utils/xml'
  import { initInputCounters } from 'flowbite'
  import { Field } from 'vee-validate'
  import { uuid } from 'vue-uuid'
  const props = defineProps({
    textModel: {
      type: String,
      required: false,
      default: '',
    },
    isShowLabel: {
      type: Boolean,
      required: false,
      default: false,
    },
    xmlColumn: {
      type: Object as PropType<xmlColumn>,
      required: true,
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
    (e: 'on-add-cart'): void
  }

  const emit = defineEmits<Emits>()

  const fieldId = uuid.v4()
  const fieldRules = computed(() => props.xmlColumn.rules)
  const timer = ref<ReturnType<typeof setTimeout> | null>(null)
  const onClickQuantity = () => {
    onFocus()
    requestAnimationFrame(() => {
      const inputEl = document.getElementById(fieldId) as HTMLInputElement
      onFocus()
      inputEl?.blur()
    })
  }

  const model = ref(props.textModel)

  const onBlur = () => {
    const inputElement = document.getElementById(fieldId) as HTMLInputElement
    model.value = model.value.replaceAll(model.value, inputElement.value)
    if (model.value === null) {
      model.value = ''
    }
    model.value = model.value.slice(0, props.maxlength)
    nextTick(() => {
      emit('on-blur')
    })
    if (timer.value) {
      clearTimeout(timer.value)
    }
    timer.value = setTimeout(() => {
      emit('on-add-cart')
      timer.value = null
    }, 500)
  }

  const onChange = () => {
    nextTick(() => {
      emit('on-change')
    })
  }

  const onInput = (event: Event) => {
    nextTick(() => {
      emit('on-input')
    })
  }

  const onFocus = () => {
    const id = document.getElementById(fieldId) as HTMLInputElement
    id.focus()
    id.select()
  }

  onMounted(() => {
    initInputCounters()
  })
</script>
