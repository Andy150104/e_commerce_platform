<template>
  <Button :label="labelButton" icon="pi pi-trash" severity="danger" outlined @click="confirmDeleteSelected" :disabled="disabled" />
  <Dialog v-model:visible="deleteProductsDialog" :style="{ width: '450px' }" header="Confirm" :modal="true">
    <div class="flex items-center gap-4">
      <i class="pi pi-exclamation-triangle !text-3xl" />
      <span>{{ content }}</span>
    </div>
    <template #footer>
      <Button :label="labelCancelButton" icon="pi pi-times" text @click="deleteProductsDialog = false" />
      <Button :label="labelOkButton" icon="pi pi-check" text @click="confirmFunction" />
    </template>
  </Dialog>
</template>
<script lang="ts" setup>
  const props = defineProps({
    labelButton: {
      type: String,
      required: false,
      default: 'Delete',
    },
    labelOkButton: {
      type: String,
      required: false,
      default: 'Yes',
    },
    labelCancelButton: {
      type: String,
      required: false,
      default: 'No',
    },
    content: {
      type: String,
      required: false,
      default: 'Are you sure you want to delete',
    },
    disabled: {
      type: Boolean,
      required: false,
      default: false,
    },
  })

  interface Emits {
    (e: 'on-open'): void
    (e: 'on-confirm'): void
  }

  const emit = defineEmits<Emits>()

  const deleteProductsDialog = ref(false)

  const confirmDeleteSelected = () => {
    emit('on-open')
    deleteProductsDialog.value = true
  }

  const confirmFunction = () => {
    emit('on-confirm')
    deleteProductsDialog.value = false
  }
</script>
