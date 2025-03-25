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
      <div class="flex items-center gap-4 w-64">
        <UserControlDateField
          :xml-column="xmlColumns.Date"
          :err-msg="fieldErrors.Date"
          :disabled="false"
          :is-show-label="false"
          :maxlength="10"
          :date-model="parsedDateString"
          :is-inline="true"
          :date-picker-position="'bottom left'"
        />
        <Button security="contrast" @click="onViewData">View</Button>
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
            <h3 class="mb-3 text-base md:text-xl font-semibold text-gray-600 dark:text-gray-300">Total Revenue</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">{{ moneyFormatter(store.DashBoardEntity.totalRevenue ?? 0) }}</p>
            <!-- <p class="mt-1 text-xs text-green-500">+3% so với tháng trước</p> -->
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
            <h3 class="mb-3 text-base md:text-xl font-semibold text-gray-600 dark:text-gray-300">Month Revenue</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">
              {{ moneyFormatter(store.DashBoardEntity.totalRevenueThisMonth ?? 0) }}
            </p>
            <p class="mt-1 text-xs text-green-500">{{ '+' + store.DashBoardEntity.revenueGrowthRateLastMonth + '% so với tháng trước' }}</p>
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
            <h3 class="mb-3 text-base md:text-xl font-semibold text-gray-600 dark:text-gray-300">Today Revenue</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">{{ moneyFormatter(store.DashBoardEntity.totalRevenueToday ?? 0) }}</p>
            <p class="mt-1 text-xs text-green-500">{{ '+' + store.DashBoardEntity.revenueGrowthRateYesterday + '% so với hôm qua' }}</p>
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
            <h3 class="mb-3 text-base md:text-xl font-semibold text-gray-600 dark:text-gray-300">Total Orders</h3>
            <p class="text-xl font-bold text-gray-800 dark:text-white">{{ store.DashBoardEntity.totalOrder + ' Orders' }}</p>
            <!-- <p class="mt-1 text-xs text-green-500">+5% so với tháng trước</p> -->
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
            <div class="flex items-center justify-between mb-4">
              <h3 class="text-lg font-semibold text-gray-800 dark:text-white">Total Revenue</h3>
              <BaseControlSelectInput
                :xml-column="xmlColumns.sortBy"
                :model="fieldValues.sortBy"
                :disabled="false"
                :master-name="'SortByChart'"
                class="w-48"
              />
            </div>
            <p class="mb-4 text-gray-700 dark:text-gray-300">
              <span class="text-2xl font-bold text-gray-900 dark:text-white">{{ moneyFormatter(store.DashBoardEntity.totalRevenue ?? 0) }}</span>
              <!-- <span class="ml-2 text-green-500">(+9% so với tháng trước)</span> -->
            </p>
            <!-- Placeholder chart -->
            <div class="items-center justify-center rounded-md bg-gray-100 dark:bg-gray-700">
              <div>
                <BaseControlBarChart class="animate-fade-right animate-once animate-duration-1000" v-if="store.DashBoardEntity.revenueData" :chartData="singleDatasetChart" />
              </div>
            </div>
          </div>

          <!-- Popular products -->
          <div
            class="animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out rounded-lg border border-gray-200 bg-white p-6 shadow-lg hover:shadow-2xl transition-shadow dark:border-gray-800 dark:bg-gray-800"
          >
            <div>
              <Tabs value="0">
                <TabList>
                  <Tab value="0">Products</Tab>
                  <Tab value="1">Product chart</Tab>
                </TabList>
                <TabPanels>
                  <TabPanel value="0">
                    <h3 class="animate-fade-left animate-once animate-duration-1000 mb-2 text-lg font-semibold text-gray-800 dark:text-white">
                      Popular Products
                    </h3>
                    <ul class="animate-fade-left animate-once animate-duration-1000 space-y-2 text-gray-700 dark:text-gray-300">
                      <li v-for="item in store.DashBoardEntity.accessoryData" class="flex items-center justify-between">
                        <span>{{ item.accessoryName }}</span>
                        <span class="rounded-full bg-green-100 px-2 py-0.5 text-sm text-green-700 dark:bg-green-800 dark:text-green-200">
                          {{ item.totalSale }} products</span
                        >
                      </li>
                    </ul>
                  </TabPanel>
                  <TabPanel value="1">
                    <BaseControlBarChart
                      class="animate-fade-right animate-once animate-duration-1000"
                      :is-horizontal="true"
                      :chartData="accessoryDatasetChart"
                    />
                  </TabPanel>
                </TabPanels>
              </Tabs>
            </div>
          </div>
        </section>
      </main>
    </template>
  </BaseScreenManage>
</template>

<script setup lang="ts">
  import BaseControlSelectInput from '@PKG_SRC/components/Basecontrol/BaseControlSelectInput.vue'
  import BaseControlBarChart from '@PKG_SRC/components/Chart/BaseControlBarChart.vue'
  import UserControlDateField from '@PKG_SRC/components/UserControl/UserControlDateField.vue'
  import BaseScreenManage from '@PKG_SRC/layouts/Basecreen/BaseScreenManage.vue'
  import { useMODManageOrderStore } from '@PKG_SRC/stores/Modules/Manage/MODManageOrderStore'
  import { Currency, Locale } from '@PKG_SRC/types/enums/constantFrontend'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'
import { stringifyQuery } from 'vue-router'

  const store = useMODManageOrderStore()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const allRevenueData = ref<number[]>([])
  const allMonths = [
    'Tháng 1',
    'Tháng 2',
    'Tháng 3',
    'Tháng 4',
    'Tháng 5',
    'Tháng 6',
    'Tháng 7',
    'Tháng 8',
    'Tháng 9',
    'Tháng 10',
    'Tháng 11',
    'Tháng 12',
  ]
  const xmlColumns = {
    Date: XmlLoadColumn({
      id: 'Date',
      name: 'Date',
      rules: '',
      visible: true,
      option: '',
    }),
    sortBy: XmlLoadColumn({
      id: 'sortBy',
      name: 'sortBy',
      rules: '',
      visible: true,
      option: '',
    }),
  }
  const singleDatasetChart = ref({
    labels: [] as string[],
    datasets: [] as any[],
  })

  const accessoryDatasetChart = ref({
    labels: [] as string[],
    datasets: [] as any[],
  })

  const parsedDateString = computed(() => {
    return fieldValues.value.Date
  })

  const moneyFormatter = (money?: number) => {
    if (money) return formatMoney(money, Currency.VND, Locale.VI_VN)
  }

  const onViewData = async () => {
    await store.GetDashBoardManageOrder()
  }

  watch(
    () => store.fieldValues.sortBy,
    async (newVal, oldVal) => {
      if (newVal !== oldVal) {
        store.fields.setFieldValue('sortBy', newVal)
        let slicedLabels: string[] = []
        let slicedData: number[] = []
        const val = Number(newVal)
        switch (val) {
          case 0: // "12 Months"
            slicedLabels = allMonths
            slicedData = allRevenueData.value
            break
          case 1: // "First 6 Months"
            slicedLabels = allMonths.slice(0, 6)
            slicedData = allRevenueData.value.slice(0, 6)
            break
          case 2: // "Later 6 Months"
            slicedLabels = allMonths.slice(6, 12)
            slicedData = allRevenueData.value.slice(6, 12)
            break
          case 3: // "1-3 Months"
            slicedLabels = allMonths.slice(0, 3)
            slicedData = allRevenueData.value.slice(0, 3)
            break
          case 4: // "4-6 Months"
            slicedLabels = allMonths.slice(3, 6)
            slicedData = allRevenueData.value.slice(3, 6)
            break
          case 5: // "7-9 Months"
            slicedLabels = allMonths.slice(6, 9)
            slicedData = allRevenueData.value.slice(6, 9)
            break
          case 6: // "10-12 Months"
            slicedLabels = allMonths.slice(9, 12)
            slicedData = allRevenueData.value.slice(9, 12)
            break
          default:
            slicedLabels = allMonths
            slicedData = allRevenueData.value
        }

        singleDatasetChart.value.labels = slicedLabels
        singleDatasetChart.value.datasets[0].data = slicedData
      }
    }
  )

  onMounted(async () => {
    await store.GetDashBoardManageOrder()
    allRevenueData.value = store.DashBoardEntity.revenueData ?? []
    singleDatasetChart.value.labels = allMonths
    singleDatasetChart.value.datasets = [
      {
        type: 'bar',
        label: 'Doanh thu (triệu đồng)',
        backgroundColor: '#42A5F5',
        data: allRevenueData.value,
      },
    ]
    const accessoryData = store.DashBoardEntity.accessoryData ?? []
    accessoryDatasetChart.value.labels = accessoryData.map((item) => item.accessoryName ?? '')
    accessoryDatasetChart.value.datasets = [
      {
        type: 'bar',
        label: 'Total sale products',
        backgroundColor: '#42A5F5',
        data: accessoryData.map((item) => item.totalSale),
      },
    ]
  })

  onUnmounted(() => {
    store.$reset()
  })
</script>
