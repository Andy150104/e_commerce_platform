<template>
  <div class="flex items-center">
    <template v-for="index in 5" :key="index">
      <!-- Sao đầy -->
      <svg
        v-if="index <= Math.floor(rating)"
        class="shrink-0 w-5 h-5 text-yellow-400 dark:text-yellow-600"
        xmlns="http://www.w3.org/2000/svg"
        fill="currentColor"
        viewBox="0 0 16 16"
      >
        <path
          d="M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.282.95l-3.522 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"
        ></path>
      </svg>

      <!-- Sao nửa -->
      <svg
        v-else-if="index - rating < 1"
        class="shrink-0 w-5 h-5 text-yellow-400 dark:text-yellow-600"
        xmlns="http://www.w3.org/2000/svg"
        fill="currentColor"
        viewBox="0 0 16 16"
      >
        <defs>
          <linearGradient id="halfStar">
            <stop offset="50%" stop-color="currentColor" />
            <stop offset="50%" stop-color="rgb(209,213,219)" />
          </linearGradient>
        </defs>
        <path
          d="M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.282.95l-3.522 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"
          fill="url(#halfStar)"
        ></path>
      </svg>

      <!-- Sao trống -->
      <svg
        v-else
        class="shrink-0 w-5 h-5 text-gray-300 dark:text-neutral-600"
        xmlns="http://www.w3.org/2000/svg"
        fill="currentColor"
        viewBox="0 0 16 16"
      >
        <path
          d="M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.282.95l-3.522 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"
        ></path>
      </svg>
    </template>
  </div>
</template>
<script setup lang="ts">
  const props = defineProps({
    rating: {
      type: Number,
      required: false,
      default: 0,
    },
  })
</script>
