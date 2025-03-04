<template>
  <div class="flex min-h-screen items-center justify-center">
    <div>
      <!-- Sidebar Overlay -->
      <div x-show="open" class="fixed inset-0 z-50 overflow-hidden">
        <div class="bg-opacity-75 absolute inset-0 bg-black transition-opacity"></div>
        <!-- Sidebar Content -->
        <section
          class="animate-fade-left animate-once animate-duration-200 animate-ease-linear absolute inset-y-0 right-0 flex w-screen max-w-3xl pl-10"
        >
          <div class="w-screen max-w-3xl">
            <div class="flex h-full flex-col bg-white py-6 shadow-xl">
              <!-- Sidebar Header -->
              <div class="flex items-center justify-between px-4">
                <h2 class="text-xl font-semibold text-black">{{ textTitle }}</h2>
                <button @click="emit('on-click-close', false)" class="text-gray-500 hover:text-gray-700">
                  <span class="sr-only">Close</span>
                  <svg
                    class="h-6 w-6"
                    x-description="Heroicon name: x"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                    aria-hidden="true"
                  >
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
                  </svg>
                </button>
              </div>
              <div class="mt-4 px-4">
                <p class="ml-2 text-gray-400">Results</p>
              </div>
              <!-- Sidebar Content -->
              <div class="mt-4 h-full overflow-auto px-4">
                <slot name="bodySideBar"></slot>
              </div>
              <!-- Sidebar Footer -->
              <div class="mt-6 px-4 flex justify-between items-center">
                <button class="flex items-center justify-center gap-1 rounded-md bg-black p-2 text-sm text-white">
                  <svg width="1rem" height="1rem" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
                    <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g>
                    <g id="SVGRepo_iconCarrier">
                      <path
                        fill-rule="evenodd"
                        clip-rule="evenodd"
                        d="M3 7C3 6.44772 3.44772 6 4 6H20C20.5523 6 21 6.44772 21 7C21 7.55228 20.5523 8 20 8H4C3.44772 8 3 7.55228 3 7ZM6 12C6 11.4477 6.44772 11 7 11H17C17.5523 11 18 11.4477 18 12C18 12.5523 17.5523 13 17 13H7C6.44772 13 6 12.5523 6 12ZM9 17C9 16.4477 9.44772 16 10 16H14C14.5523 16 15 16.4477 15 17C15 17.5523 14.5523 18 14 18H10C9.44772 18 9 17.5523 9 17Z"
                        fill="currentColor"
                      ></path>
                    </g>
                  </svg>
                  Filters
                </button>
                <div class="flex items-center gap-2 bg-gray-100 px-4 py-2 rounded-md shadow-md">
                  <span class="text-sm font-medium text-gray-500">Total:</span>
                  <span class="text-lg font-bold text-black"> VND </span>
                </div>
              </div>
            </div>
          </div>
        </section>
      </div>
    </div>
  </div>
</template>
<script lang="ts" setup>
  const props = defineProps({
    textTitle: {
      type: String,
      required: false,
      default: 'Title',
    },
  })

  interface Emits {
    (e: 'on-click-close', value: boolean): void
  }
  const emit = defineEmits<Emits>()
</script>
