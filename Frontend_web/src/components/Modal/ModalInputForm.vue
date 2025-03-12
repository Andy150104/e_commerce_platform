<template>
  <div>
    <Button v-if="isOnlyShowIcon" :icon="buttonIcon" outlined rounded :severity="buttonSeverity" @click="visible = true" />
    <Button v-else :label="buttonLabel" @click="visible = true" :disabled="disabled" :outlined="outlined" :icon="buttonIcon" :severity="buttonSeverity" />
    <Dialog
      v-model:visible="visible"
      :header="header"
      :style="{ width: width }"
      :breakpoints="breakpoints"
      :maximizable="maximizable"
      modal
      dismissableMask
      appendTo="body"
    >
      <!-- Slot Header -->
      <template #header>
        <slot name="header">
          <h2 class="font-bold" v-html="header"></h2>
        </slot>
      </template>

      <!-- Slot Body (Nội dung chính) -->
      <div class="p-4">
        <slot name="body">
          <p class="text-gray-500">Default content here. Override this by using slot.</p>
        </slot>
      </div>

      <!-- Slot Footer -->
      <template #footer>
        <slot name="footer">
          <Button :label="cancelLabel" text severity="secondary" @click="visible = false" />
          <Button :label="confirmLabel" outlined severity="primary" @click="onConfirm" />
        </slot>
      </template>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
  import { ref } from 'vue'

  const props = defineProps({
    isOnlyShowIcon:{
      type: Boolean,
      required: false,
      default: false,
    },
    buttonLabel: {
      type: String,
      default: 'Show',
    },
    outlined: {
      type: Boolean,
      required: false,
      default: false,
    },
    buttonIcon: {
      type: String,
      default: 'pi pi-eye',
    },
    buttonSeverity: {
      type: String,
      default: 'contrast',
    },
    header: {
      type: String,
      default: 'Form',
    },
    width: {
      type: String,
      default: '40rem',
    },
    maximizable: {
      type: Boolean,
      default: false,
    },
    breakpoints: {
      type: Object,
      default: () => ({ '1199px': '75vw', '575px': '90vw' }),
    },
    confirmLabel: {
      type: String,
      default: 'Confirm',
    },
    cancelLabel: {
      type: String,
      default: 'Cancel',
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  })

  interface Emits {
    (e: 'on-confirm'): void
  }

  const emit = defineEmits<Emits>()

  const visible = ref(false)

  const onConfirm = () => {
    emit('on-confirm')
    visible.value = false
  }
</script>
