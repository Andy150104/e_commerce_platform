<template>
  <div class="mt-2 p-4 border border-gray-300 bg-gray-50 rounded-lg shadow">
    <div class="flex items-center space-x-4">
      <img
        :src="
          detail.userImage ||
          'https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ='
        "
        alt="Profile Picture"
        class="w-24 h-24 rounded-full border"
      />
      <div>
        <h2 class="text-xl font-semibold text-gray-700">{{ detail.userFullNameQueue || 'N/A' }}</h2>
        <p class="text-gray-600">Công ty HICAS</p>
      </div>
    </div>

    <div class="mt-4 space-y-2">
      <p class="text-gray-600"><strong>Gender: </strong>{{ detail.userGender || 'N/A' }}</p>
      <p class="text-gray-600"><strong>Birth Day: </strong>{{ detail.userBirthday || 'N/A' }}</p>
      <p class="text-gray-600"><strong>Phone: </strong>{{ detail.userPhone || 'N/A' }}</p>
    </div>

    <div
      class="mt-6 p-4 rounded-lg border border-gray-200 bg-gray-50 w-full max-h-40 overflow-y-auto overflow-x-hidden break-all whitespace-pre-wrap"
    >
      <p class="text-gray-600">
        <strong>Description: </strong>
        <span v-html="detail.descriptionQueue || 'No description available'"></span>
      </p>
    </div>

    <button
      @click="detail?.exchangeId && detail?.queueId && emit('approve', detail.exchangeId, detail.queueId)"
      class="mt-4 w-full rounded bg-red-500 px-4 py-2 font-medium text-white hover:bg-red-600"
    >
      Approve Exchange
    </button>
  </div>
</template>

<script setup lang="ts">
  interface Detail {
    userImage?: string
    userFullNameQueue?: string
    userGender?: string
    userBirthday?: string
    userPhone?: string
    descriptionQueue?: string
    exchangeId: string
    queueId: string
  }

  defineProps<{
    detail: Detail
  }>()

  interface Emits {
    (e: 'approve', exchangeId: string, queueId: string): void
  }
  const emit = defineEmits<Emits>()
</script>
