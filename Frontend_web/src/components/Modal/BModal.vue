<template>
  <div>
    <UModal v-model="isOpen">
      <UCard :ui="{ ring: '', divide: 'divide-y divide-gray-100 dark:divide-gray-800' }">
        <template #header>
          <h2>{{ labelModelName }}</h2>
        </template>

        <slot />

        <template #footer>
          <slot name="footer" />
        </template>
      </UCard>
    </UModal>
  </div>
</template>

<script setup lang="ts">
  import { ref, defineProps, defineEmits } from 'vue'

  const props = defineProps({
    labelModelName: {
      type: String,
      required: true,
      default: '',
    },
    modelValue: Boolean, // Nhận giá trị từ cha
  })

  const emit = defineEmits(['update:modelValue'])

  const isOpen = ref(props.modelValue)

  // Khi modal thay đổi trạng thái, báo cho cha biết
  watch(() => props.modelValue, (newVal) => {
    isOpen.value = newVal
  })

  watch(isOpen, (newVal) => {
    emit('update:modelValue', newVal)
  })
</script>
