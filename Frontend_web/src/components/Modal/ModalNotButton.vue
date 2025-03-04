<template>
  <div v-if="isShowPopup" class="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50 z-50" @click.self="closePopup(false)">
    <div class="animate-jump-in animate-once animate-ease-out relative p-4 w-full max-w-xl max-h-xl">
      <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
        <button
          type="button"
          class="absolute top-3 end-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
          data-modal-hide="popup-modal"
          @click="closePopup(false)"
        >
          <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6" />
          </svg>
          <span class="sr-only">Close modal</span>
        </button>
        <div class="p-4 md:p-5 text-center">
          <svg
            class="mx-auto mb-4 text-gray-400 w-12 h-12 dark:text-gray-200"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 20 20"
          >
            <path
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M10 11V6m0 8h.01M19 10a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"
            />
          </svg>
          <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">{{ message }}</h3>
          <button data-modal-hide="popup-modal" type="button" :class="classNameButtonOk" @click="emit('on-click')">{{ buttonOKName }}</button>
          <button
            data-modal-hide="popup-modal"
            type="button"
            class="py-2.5 px-5 ms-3 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:ring-gray-100 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
            @click="closePopup(false)"
          >
            {{ buttonCancelName }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts" setup>
  import { className } from '@PKG_SRC/utils/class/className'

  const isShowPopup = ref(false)

  const props = defineProps({
    buttonOKName: {
      type: String,
      required: true,
      default: 'Yes',
    },
    buttonCancelName: {
      type: String,
      required: true,
      default: 'Cancel',
    },
    message: {
      type: String,
      required: true,
      default: 'Are you sure you want to delete this product?',
    },
    classNameButtonOk: {
      type: String,
      required: true,
      default: className.BUTTON_GRADIENT_BLUE_1,
    },
  })

  interface Emits {
    (e: 'on-click'): void
  }

  const emit = defineEmits<Emits>()

  function closePopup(value: boolean) {
    isShowPopup.value = value
  }

  defineExpose({
    closePopup,
  })
</script>
