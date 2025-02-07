<template>
  <div>
    <UButton v-if="isShowButton" :label="label" @click="isOpenModal" />

    <UModal
      v-model="isOpen"
      fullscreen
      :ui="{
        base: 'h-full flex flex-col',
        rounded: '',
        divide: 'divide-y divide-gray-100 dark:divide-gray-800',
        body: {
          base: 'grow',
        },
        width: 'w-full',
        height: 'h-screen overflow-y-auto max-h-screen',
      }"
    >
      <UCard
        :ui="{
          base: 'fixed inset-0 z-50 bg-white dark:bg-gray-800 overflow-y-auto flex flex-col justify-between',
          rounded: '',
          divide: 'divide-y divide-gray-100 dark:divide-gray-800',
          body: {
            base: 'grow',
          },
        }"
      >
        <template #header>
          <div class="flex items-center justify-between p-4 border-b border-gray-200 dark:border-gray-700">
            <h3 class="text-base font-semibold leading-6 text-gray-900 dark:text-white text-center">Modal</h3>
            <UButton color="gray" variant="ghost" icon="i-heroicons-x-mark-20-solid" class="-my-1" @click="isCloseModal" />
          </div>
        </template>
        <div class="flex-grow w-full h-auto p-6">
          <slot name="body"></slot>
        </div>
        <template #footer>
          <div class="p-4 border-t border-gray-200 dark:border-gray-700">
          </div>
        </template>
      </UCard>
    </UModal>
  </div>
</template>
<script lang="ts" setup>
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
  .custom-modal {
    margin: 0px !important;
  }
</style>
