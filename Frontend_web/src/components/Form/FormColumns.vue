<template>
  <div :class="'grid gap-6 mb-6 mt-3 ' + cols">
    <slot></slot>
  </div>
</template>
<script setup lang="ts">
  const props = defineProps({
    cols: {
      type: String,
      required: false,
      default: '',
    },
  })
</script>
