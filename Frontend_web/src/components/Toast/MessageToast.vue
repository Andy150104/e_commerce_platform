<template>
  <UNotifications :ui="{ base: 'absolute top-0 end-0 start-0 h-1' }" />
</template>
<script setup lang="ts">
  import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'

  const toast = useToast()
  const formMessageStore = useFormMessageStore()

  onMounted(() => {
    if (formMessageStore.isAction) {
      toast.add({
        id: formMessageStore.messageId,
        title: 'Message',
        description: 'It will be installed on restart. Restart now?',
        icon: 'i-heroicons-check-badge',
        timeout: 1200,
        ui: {
          process: {
            base: 'absolute top-0 end-0',
          },
        },
      })
    }
    toast.add({
      id: formMessageStore.messageId,
      title: 'Message',
      description: 'It will be installed on restart. Restart now?',
      icon: 'i-heroicons-check-badge',
      timeout: 1200,
      ui: {
        base: 'absolute top-0 end-0',
      },
      actions: [
        {
          label: 'Restart',
          click: () => {},
        },
      ],
    })
  })
</script>
