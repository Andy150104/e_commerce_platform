<template>
  <UNotifications :ui="{ base: 'absolute top-0 end-0 start-0 h-1' }" />
</template>
<script setup lang="ts">
  import { onMounted } from 'vue'
  import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'

  const toast = useToast()
  const formMessageStore = useFormMessageStore()

  onMounted(() => {
    if (!formMessageStore.isNotify) return

    const timeout = 1400 // Thời gian hiển thị
    const isError = formMessageStore.messageId.startsWith('E')

    // Cấu hình chung
    const baseOptions = {
      id: formMessageStore.messageId,
      title: isError ? 'Error' : 'Message',
      description: formMessageStore.message,
      icon: isError ? 'i-heroicons-exclamation-circle' : 'i-heroicons-check-badge',
      timeout,
      color: isError ? 'error' : 'success',
      ui: { base: 'absolute top-0 end-0' },
    }

    // Thêm toast (dùng spread để gọn)
    toast.add({
      ...baseOptions,
      ...(formMessageStore.isAction && {
        actions: [{ label: 'Retry', click: () => {} }],
      }),
    })

    // Reset sau khi thông báo biến mất
    setTimeout(formMessageStore.ResetStore, timeout)
  })
</script>
