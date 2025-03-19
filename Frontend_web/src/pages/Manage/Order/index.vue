<template>
  <BaseScreenManage>
    <template #body>
      <!-- Thêm font-sans, leading-relaxed để tăng line-height, text-gray-900 và dark:text-white để chữ luôn dễ đọc -->
      <div class="max-w-3xl text-start relative z-10 mt-0 pt-0 font-sans leading-relaxed text-gray-900 dark:text-white">
        <!-- Heading lớn: dùng text-3xl hoặc text-4xl, có thể để gradient nếu muốn tạo điểm nhấn -->
        <h1
          class="text-4xl font-medium tracking-wide text-transparent bg-clip-text bg-gradient-to-r from-blue-600 to-indigo-600 mb-4 animate-fade-up animate-duration-1000 animate-delay-300"
        >
          Dashboard Revenue
        </h1>

        <!-- Đoạn chào: dùng text-lg hoặc text-base + màu chữ trung bình (xám) -->
        <p class="text-base sm:text-lg text-gray-700 dark:text-gray-300 mb-8 animate-fade-up animate-duration-1000 animate-delay-500">
          Welcome, Admin!
        </p>
      </div>

      <!-- Nền với gradient tinh tế -->
      <main
        class="animate-flip-up animate-once animate-ease-out animate-normal animate-fill-forwards min-h-screen bg-gradient-to-br from-blue-50 to-white p-6 dark:from-gray-900 dark:to-gray-800 dark:text-white space-y-6"
      >
        <!-- Thông tin tổng quan (cards) -->
        <section
          class="animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out grid grid-cols-1 gap-6 sm:grid-cols-2 md:grid-cols-4"
        >
          <!-- Card 1 -->
          <div
            class="animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out relative rounded-lg border-l-4 border-blue-500 bg-gradient-to-r from-white to-blue-50 p-6 shadow-lg hover:shadow-2xl transition-all duration-300 hover:scale-105 dark:from-gray-800 dark:to-gray-700"
          >
            <div
              class="absolute -top-4 -left-4 flex h-12 w-12 items-center justify-center rounded-full bg-blue-100 text-blue-600 dark:bg-blue-800 dark:text-blue-100 shadow-md"
            >
              <!-- Icon -->
              <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 10h4l3 10h8l3-10h4" />
              </svg>
            </div>
            <!-- Giảm kích thước tiêu đề, con số và phần trăm -->
            <h3 class="mb-3 text-xs font-semibold text-gray-600 dark:text-gray-300">Sales Performance</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">$23,127</p>
            <p class="mt-1 text-xs text-green-500">+3% so với tháng trước</p>
          </div>

          <!-- Card 2 -->
          <div
            class="animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out relative rounded-lg border-l-4 border-orange-500 bg-gradient-to-r from-white to-orange-50 p-6 shadow-lg hover:shadow-2xl transition-all duration-300 hover:scale-105 dark:from-gray-800 dark:to-gray-700"
          >
            <div
              class="absolute -top-4 -left-4 flex h-12 w-12 items-center justify-center rounded-full bg-orange-100 text-orange-600 dark:bg-orange-800 dark:text-orange-100 shadow-md"
            >
              <!-- Icon -->
              <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M12 8c-1.104 0-2 .896-2 2 
                       0 1.104.896 2 2 2 .372 0 .714-.101 1.008-.264
                       M14 4h2c1.104 0 2 .896 2 2v2m-2 8v2c0 1.104-.896 2-2 2H6
                       c-1.104 0-2-.896-2-2v-2m12 0H4"
                />
              </svg>
            </div>
            <h3 class="mb-3 text-xs font-semibold text-gray-600 dark:text-gray-300">Total Sales</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">1,849</p>
            <p class="mt-1 text-xs text-green-500">+3% so với tháng trước</p>
          </div>

          <!-- Card 3 -->
          <div
            class="animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out relative rounded-lg border-l-4 border-purple-500 bg-gradient-to-r from-white to-purple-50 p-6 shadow-lg hover:shadow-2xl transition-all duration-300 hover:scale-105 dark:from-gray-800 dark:to-gray-700"
          >
            <div
              class="absolute -top-4 -left-4 flex h-12 w-12 items-center justify-center rounded-full bg-purple-100 text-purple-600 dark:bg-purple-800 dark:text-purple-100 shadow-md"
            >
              <!-- Icon -->
              <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M11 11V5a1 1 0 012 0v6a1 1 0 01-2 0z
                       M5 11v6a1 1 0 102 0v-6a1 1 0 10-2 0z
                       M15 11v4a1 1 0 102 0v-4a1 1 0 10-2 0z"
                />
              </svg>
            </div>
            <h3 class="mb-3 text-xs font-semibold text-gray-600 dark:text-gray-300">Average Revenue</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">$15,239</p>
            <p class="mt-1 text-xs text-green-500">+9% so với tháng trước</p>
          </div>

          <!-- Card 4 -->
          <div
            class="animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out relative rounded-lg border-l-4 border-green-500 bg-gradient-to-r from-white to-green-50 p-6 shadow-lg hover:shadow-2xl transition-all duration-300 hover:scale-105 dark:from-gray-800 dark:to-gray-700"
          >
            <div
              class="absolute -top-4 -left-4 flex h-12 w-12 items-center justify-center rounded-full bg-green-100 text-green-600 dark:bg-green-800 dark:text-green-100 shadow-md"
            >
              <!-- Icon -->
              <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 10h16M4 14h16M4 18h16" />
              </svg>
            </div>
            <h3 class="mb-3 text-xs font-semibold text-gray-600 dark:text-gray-300">Total Sessions</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">2,034</p>
            <p class="mt-1 text-xs text-green-500">+5% so với tháng trước</p>
          </div>
        </section>

        <!-- Biểu đồ và danh sách sản phẩm -->
        <section
          class="animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out grid grid-cols-1 gap-6 md:grid-cols-3"
        >
          <!-- Biểu đồ -->
          <div
            class="col-span-2 rounded-lg border border-gray-200 bg-white p-6 shadow-lg hover:shadow-2xl transition-shadow dark:border-gray-800 dark:bg-gray-800"
          >
            <h3 class="mb-2 text-lg font-semibold text-gray-800 dark:text-white">Total Revenue</h3>
            <p class="mb-4 text-gray-700 dark:text-gray-300">
              <span class="text-2xl font-bold text-gray-900 dark:text-white">$94,127</span>
              <span class="ml-2 text-green-500">(+9% so với tháng trước)</span>
            </p>
            <!-- Placeholder chart -->
            <div class="items-center justify-center rounded-md bg-gray-100 dark:bg-gray-700">
                <div>
                    <BaseControlBarChart />
                </div>
            </div>
          </div>

          <!-- Popular products -->
          <div
            class="animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out rounded-lg border border-gray-200 bg-white p-6 shadow-lg hover:shadow-2xl transition-shadow dark:border-gray-800 dark:bg-gray-800"
          >
            <h3 class="mb-2 text-lg font-semibold text-gray-800 dark:text-white">Popular Products</h3>
            <ul class="space-y-2 text-gray-700 dark:text-gray-300">
              <li class="flex items-center justify-between">
                <span>Macbook Pro 16 M1</span>
                <span class="rounded-full bg-green-100 px-2 py-0.5 text-sm text-green-700 dark:bg-green-800 dark:text-green-200"> 45% </span>
              </li>
              <li class="flex items-center justify-between">
                <span>iPhone 12</span>
                <span class="rounded-full bg-green-100 px-2 py-0.5 text-sm text-green-700 dark:bg-green-800 dark:text-green-200"> 30% </span>
              </li>
              <li class="flex items-center justify-between">
                <span>Macbook Air M1</span>
                <span class="rounded-full bg-green-100 px-2 py-0.5 text-sm text-green-700 dark:bg-green-800 dark:text-green-200"> 15% </span>
              </li>
              <li class="flex items-center justify-between">
                <span>Apple 24" M1 iMac Pro Display XDR</span>
                <span class="rounded-full bg-green-100 px-2 py-0.5 text-sm text-green-700 dark:bg-green-800 dark:text-green-200"> 10% </span>
              </li>
            </ul>
          </div>
        </section>

        <!-- Thông tin dưới (cards) -->
        <section
          class="animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out grid grid-cols-1 gap-6 sm:grid-cols-2 md:grid-cols-3"
        >
          <div
            class="rounded-lg border border-gray-200 bg-white p-6 shadow-lg hover:shadow-2xl transition-shadow dark:border-gray-800 dark:bg-gray-800"
          >
            <h3 class="mb-3 text-xs font-semibold text-gray-600 dark:text-gray-300">Average Order Value</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">$992</p>
            <p class="mt-1 text-xs text-green-500">+2% so với tháng trước</p>
          </div>
          <div
            class="rounded-lg border border-gray-200 bg-white p-6 shadow-lg hover:shadow-2xl transition-shadow dark:border-gray-800 dark:bg-gray-800"
          >
            <h3 class="mb-3 text-xs font-semibold text-gray-600 dark:text-gray-300">Average Sales</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">840</p>
            <p class="mt-1 text-xs text-green-500">+1.3% so với tháng trước</p>
          </div>
          <div
            class="rounded-lg border border-gray-200 bg-white p-6 shadow-lg hover:shadow-2xl transition-shadow dark:border-gray-800 dark:bg-gray-800"
          >
            <h3 class="mb-3 text-xs font-semibold text-gray-600 dark:text-gray-300">Total Visitors</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">11,240</p>
            <p class="mt-1 text-xs text-green-500">+32.5% so với tháng trước</p>
          </div>
        </section>
      </main>
    </template>
  </BaseScreenManage>
</template>

<script setup lang="ts">
  import BaseControlBarChart from '@PKG_SRC/components/Chart/BaseControlBarChart.vue';
import BaseScreenManage from '@PKG_SRC/layouts/Basecreen/BaseScreenManage.vue'
</script>
