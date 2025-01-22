<template>
  <div>
    <UButton :label="labelModelName" @click="isOpen = true" />

    <UModal v-model="isOpen">
      <UCard :ui="{ ring: '', divide: 'divide-y divide-gray-100 dark:divide-gray-800' }">
        <template #header>
          <Placeholder class="h-8"> aaaa </Placeholder>
        </template>

        <Placeholder class="h-32" />

        <template #footer>
          <Placeholder class="h-8" />
        </template>
      </UCard>
    </UModal>
  </div>
</template>
<script setup lang="ts">
  import { ref } from 'vue'
  const isOpen = ref(false)
  const props = defineProps({
    labelModelName: {
      type: String,
      required: true,
      default: '',
    },
  })
</script>
<style lang=""></style>
