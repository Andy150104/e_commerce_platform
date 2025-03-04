<template>
  <!-- <UNotifications /> -->
  <Toast class="z-[1000]" />
</template>
<script setup lang="ts">
  import { useToast } from 'primevue/usetoast'
  import { onMounted } from 'vue'
  import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'

  const toast = useToast()
  const formMessageStore = useFormMessageStore()

  watch(
    () => formMessageStore.messageId,
    (newVal) => {
      if (!newVal) return

      const timeout = 1400
      const isError = formMessageStore.messageId.startsWith('E')

      if (formMessageStore.isNotify) {
        toast.add({
          severity: isError ? 'error' : 'success',
          summary: isError ? 'Error Message' : 'Message',
          detail: formMessageStore.message,
          life: timeout,
        })
      }
      formMessageStore.ResetStore()
    }
  )
</script>
