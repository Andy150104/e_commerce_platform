<template>
  <div class="hidden md:flex justify-center items-center space-x-4">
    <div
      v-for="(plan, index) in store.planItems"
      :key="index"
      @click="selectedPlan = index"
      :class="[
        'cursor-pointer bg-white dark:bg-gray-800 shadow-md rounded-lg p-6 border',
        selectedPlan === index ? 'scale-105 border-blue-500 dark:border-blue-400' : 'border-gray-300 dark:border-gray-700',
        'transition-transform transform duration-300',
        'h-[400px] w-[300px]',
      ]"
    >
      <div class="flex items-center justify-between">
        <h3 class="text-xl font-bold text-gray-800 dark:text-white">{{ plan.planName }}</h3>
        <span class="bg-blue-100 dark:bg-blue-700 text-blue-600 dark:text-white text-xs px-2 py-1 rounded-lg">
          Most popular
        </span>
      </div>
      <p class="text-gray-500 dark:text-gray-400 mt-2 h-9">{{ plan.description }}</p>
      <div class="mt-4">
        <p class="text-3xl font-bold text-blue-600 dark:text-blue-400">{{ plan.price + " VND" }}</p>
      </div>
      <button class="w-full mt-4 bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600 dark:bg-blue-600 dark:hover:bg-blue-700" @click.stop="onBuyPlan(plan.planId ?? '')">Buy now</button>
      <ul class="mt-4 space-y-2 text-gray-600 dark:text-gray-400">
        <li class="flex items-center">
          <svg class="w-5 h-5 text-blue-500 dark:text-blue-400 mr-2" fill="currentColor" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
            <path d="M20 6L9 17l-5-5"></path>
          </svg>
          {{ plan.description }}
        </li>
        <li class="flex items-center">
          <svg class="w-5 h-5 text-blue-500 dark:text-blue-400 mr-2" fill="currentColor" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
            <path d="M20 6L9 17l-5-5"></path>
          </svg>
          {{'Duration: '+ plan.durationMonths + ' month' }}
        </li>
      </ul>
    </div>
  </div>

  <!-- Mobile Carousel -->
  <div class="md:hidden justify-center items-center">
    <swiper :slides-per-view="1" space-between="10" class="w-full flex justify-center items-center">
      <swiper-slide v-for="(plan, index) in store.planItems" :key="index" class="flex justify-center">
        <div
          :class="[
            'cursor-pointer bg-white dark:bg-gray-800 shadow-md rounded-lg p-6 border mx-auto',
            selectedPlan === index ? 'border-blue-500 dark:border-blue-400' : 'border-gray-300 dark:border-gray-700',
            'transition-transform transform duration-300 w-[300px]',
          ]"
          @click="selectedPlan = index"
        >
          <div class="flex items-center justify-between">
            <h3 class="text-xl font-bold text-gray-800 dark:text-white">{{ plan.planName }}</h3>
            <span class="bg-blue-100 dark:bg-blue-700 text-blue-600 dark:text-white text-xs px-2 py-1 rounded-lg">
              Most popular
            </span>
          </div>
          <p class="text-gray-500 dark:text-gray-400 mt-2">{{ plan.description }}</p>
          <div class="mt-4">
            <p class="text-3xl font-bold text-blue-600 dark:text-blue-400">{{ plan.price + " VND" }}</p>
          </div>
          <button class="w-full mt-4 bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600 dark:bg-blue-600 dark:hover:bg-blue-700" type="button" @click.stop="onBuyPlan(plan.planId ?? '')">
            Buy now
          </button>
          <ul class="mt-4 space-y-2 text-gray-600 dark:text-gray-400">
            <li class="flex items-center">
              <svg class="w-5 h-5 text-blue-500 dark:text-blue-400 mr-2" fill="currentColor" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                <path d="M20 6L9 17l-5-5"></path>
              </svg>
              {{ plan.description }}
            </li>
            <li class="flex items-center">
              <svg class="w-5 h-5 text-blue-500 dark:text-blue-400 mr-2" fill="currentColor" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                <path d="M20 6L9 17l-5-5"></path>
              </svg>
              {{'Duration: '+ plan.durationMonths + ' month' }}
            </li>
          </ul>
        </div>
      </swiper-slide>
    </swiper>
  </div>
</template>
<script lang="ts" setup>
  import { ref } from 'vue'
  import { Swiper, SwiperSlide } from 'swiper/vue'
  import 'swiper/swiper-bundle.css'
  import { usePlanStore } from '@PKG_SRC/stores/Modules/Plan/PlanStore'

  const store = usePlanStore()

  const onBuyPlan = async (planId: string) =>{
    if (await store.BuyPlan(planId)){
      window.location.href = store.paymentUrl
    }
  }

  const selectedPlan = ref(0)

  onMounted(async() =>{
    await store.GetAllPlan()
  })
</script>
