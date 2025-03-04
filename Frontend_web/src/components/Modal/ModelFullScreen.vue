<template>
  <div>
    <!-- Nếu isShowButton đúng thì hiển thị Button, khi click mở Dialog -->
    <Button v-if="isShowButton" :label="label" @click="() => isOpenModal(true)" />

    <!-- Dialog fullscreen với style được set tương tự -->
    <Dialog v-model:visible="isOpen" modal :style="{ width: '100vw', height: '100vh' }" class="custom-dialog">
      <template #header>
        <div class="flex items-center justify-between p-4 border-b border-gray-200 dark:border-gray-700">
          <h3 class="text-base font-semibold leading-6 text-gray-900 dark:text-white text-center">Modal</h3>
          <Button icon="pi pi-times" class="-my-1" @click="isCloseModal" />
        </div>
      </template>
      <div class="flex-grow w-full h-auto p-6">
        <slot name="body"></slot>
      </div>
      <template #footer>
        <div class="p-4 border-t border-gray-200 dark:border-gray-700"></div>
      </template>
    </Dialog>
  </div>
</template>

<script lang="ts" setup>
  import { ref } from 'vue'

  // Logic giữ nguyên như cũ
  const isOpen = ref(false)
  const isOpenModal = (isOpenModal: boolean) => {
    isOpen.value = isOpenModal
  }
  const isCloseModal = () => {
    isOpen.value = false
  }

  const props = defineProps({
    label: {
      type: String,
      required: false,
      default: 'Open',
    },
    isShowButton: {
      type: Boolean,
      required: false,
      default: true,
    },
  })

  defineExpose({
    isOpenModal,
  })
</script>

<style scoped>
  .custom-dialog {
    margin: 0 !important;
  }
</style>
