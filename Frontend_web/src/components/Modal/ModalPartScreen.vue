<template>
  <button
    data-modal-target="default-modal"
    data-modal-toggle="default-modal"
    class="block text-white bg-blue-700 hover:bg-blue-800 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700"
    type="button"
  >
    {{ labelName }}
  </button>

  <div
    id="default-modal"
    tabindex="-1"
    aria-hidden="true"
    class="hidden overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full md:inset-0 h-[calc(100%-1rem)] max-h-full animate-fade-up animate-duration-500 animate-ease-in-out"
  >
    <div class="relative p-4 w-full max-w-2xl max-h-full">
      <!-- Nội dung modal -->
      <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
        <!-- Header modal -->
        <div class="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600 border-gray-200">
          <h3 class="text-xl font-semibold text-gray-900 dark:text-white">{{ headerName }}</h3>
          <button
            type="button"
            class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
            data-modal-hide="default-modal"
          >
            <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6" />
            </svg>
            <span class="sr-only">Close modal</span>
          </button>
        </div>
        <!-- Nội dung modal -->
        <div class="p-4 md:p-5 space-y-4">
          <slot name="body"></slot>
        </div>
        <!-- Footer modal -->
        <div class="flex items-center p-4 md:p-5 border-t border-gray-200 rounded-b dark:border-gray-600">
          <!-- Lưu ý: Không đặt data-modal-hide cho nút I accept -->
          <button
            type="button"
            class="text-white bg-blue-700 hover:bg-blue-800 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700"
            @click="clickOkButton"
          >
            {{ okButtonName }}
          </button>
          <!-- Nút Decline vẫn dùng data-modal-hide để Flowbite tự đóng -->
          <button
            data-modal-hide="default-modal"
            type="button"
            class="py-2.5 px-5 ms-3 text-sm font-medium text-gray-900 bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-blue-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
          >
            {{ cancelButtonName }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
  import { onMounted } from 'vue'
  import { initModals } from 'flowbite'

  const props = defineProps({
    onOkClick: {
      type: Function as PropType<() => Promise<boolean> | boolean>,
      required: true,
    },
    headerName: {
      type: String,
      required: false,
      default: 'Add',
    },
    labelName: {
      type: String,
      required: false,
      default: 'Add',
    },
    okButtonName: {
      type: String,
      required: false,
      default: 'Add',
    },
    cancelButtonName: {
      type: String,
      required: false,
      default: 'Cancel',
    },
  })

  onMounted(() => {
    initModals()
  })

  const clickOkButton = async (event: Event) => {
    event.preventDefault()
    const result = await props.onOkClick()
    if (result !== false) {
      const closeButton = document.querySelector('[data-modal-hide="default-modal"]')
      closeButton?.dispatchEvent(new MouseEvent('click', { bubbles: true }))
    }
  }
</script>
