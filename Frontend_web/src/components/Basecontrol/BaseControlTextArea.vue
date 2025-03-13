<template>
  <Textarea v-model="internalValue" :placeholder="placeholder" :rows="rows" autoResize :style="styleCss" />
</template>

<script lang="ts" setup>
  import Textarea from 'primevue/textarea'
  import { ref, watch, computed, defineProps, defineEmits } from 'vue'

  /** Định nghĩa các props */
  const props = defineProps({
    modelValue: {
      type: String,
      default: '',
    },
    placeholder: {
      type: String,
      default: 'Normal',
    },
    rows: {
      type: Number,
      default: 1,
    },
    styleCss: {
      type: String,
      default: 'width: 100%; min-height: 30px; max-height: 150px; overflow-y: auto; resize: none;',
    },
  })

  const emit = defineEmits(['update:modelValue'])

  const internalValue = ref(props.modelValue)

  watch(
    () => props.modelValue,
    (newVal) => {
      internalValue.value = newVal
    }
  )

  watch(internalValue, (newVal) => {
    emit('update:modelValue', newVal)
  })

</script>
