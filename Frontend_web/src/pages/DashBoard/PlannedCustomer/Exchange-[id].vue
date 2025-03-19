<template>
  <BaseScreenDashBoard>
    <template #body>
      <div>
        <div class="container mx-auto px-4 py-8">
          <div class="grid grid-cols-1 gap-6 md:grid-cols-[1fr_2fr]">
            <!-- CỘT TRÁI: Danh sách thẻ (cards) - KHÔNG dùng overflow-y-auto -->
            <div class="h-full space-y-4 overflow-y-auto">
              <!-- Card 1 -->
              <div
                v-for="queueDetail in store.queueListDetail"
                @click="handleSelect(queueDetail)"
                class="animate-fade-right animate-once animate-ease-in-out"
                :class="
                  selectedQueueDetail?.queueId === queueDetail.queueId
                    ? 'relative rounded-lg border-2 border-sky-500 ring-sky-400 bg-sky-50 shadow cursor-pointer p-4'
                    : 'relative rounded-lg border border-gray-200 bg-white shadow cursor-pointer p-4'
                "
              >
                <span class="absolute top-4 right-4 rounded bg-green-400 px-2 py-1 text-xs font-semibold text-white"> New </span>
                <h2 class="mb-2 text-lg font-semibold">{{ queueDetail.userFullNameQueue }}</h2>
                <div class="mb-3 flex items-center space-x-2">
                  <span class="inline-block rounded bg-blue-200 px-2 py-1 text-sm text-gray-800">{{ 'Gender: ' + queueDetail.userGender }}</span>
                  <span class="inline-block rounded bg-yellow-200 px-2 py-1 text-sm text-gray-800">{{
                    'Birthday: ' + queueDetail.userBirthday
                  }}</span>
                  <span class="inline-block rounded bg-green-200 px-2 py-1 text-sm text-gray-800">{{ 'Phone: ' + queueDetail.userPhone }}</span>
                </div>
                <p class="mb-4 text-gray-600"></p>
                <div
                  v-if="selectedQueueDetail?.queueId === queueDetail.queueId"
                  class="animate-fade-left animate-once animate-ease-in-out block md:hidden mt-4 rounded-lg border border-gray-200 bg-white p-4 shadow"
                >
                  <h2 class="mb-2 text-xl font-semibold">
                    {{ selectedQueueDetail.userFullNameQueue }}
                  </h2>
                  <p class="text-gray-600">Công ty HICAS</p>

                  <div class="mt-4">
                    <p class="mb-2 text-gray-600">
                      <strong>Gender: </strong>{{ selectedQueueDetail.userGender }}
                    </p>
                    <p class="mb-2 text-gray-600">
                      <strong>Birth Day: </strong>{{ selectedQueueDetail.userBirthday }}
                    </p>
                    <p class="mb-2 text-gray-600">
                      <strong>Phone : </strong>{{ selectedQueueDetail.userPhone }}
                    </p>
                  </div>

                  <button class="mt-4 rounded bg-red-500 px-4 py-2 font-medium text-white hover:bg-red-600">
                    Approve Exchange
                  </button>
                </div>
              </div>
            </div>

            <!-- CỘT PHẢI: Sticky để không trôi khi trang cuộn -->
            <div
              v-if="selectedQueueDetail"
              :key="rightPanelKey"
              class="animate-fade-left animate-once animate-ease-in-out hidden md:block sticky h-full overflow-auto top-0 rounded-lg border border-gray-200 bg-white p-6 shadow"
            >
              <!-- Tiêu đề, nút Apply -->
              <div class="mb-6 flex items-start justify-between">
                <div>
                  <h2 class="mb-2 text-2xl font-semibold">{{ selectedQueueDetail.userFullNameQueue }}</h2>
                  <p class="text-gray-600">Công ty HICAS</p>
                </div>
                <button class="rounded bg-red-500 px-4 py-2 font-medium text-white hover:bg-red-600">Approve Exchange</button>
              </div>

              <!-- Thông tin tổng quan -->
              <div class="mb-6">
                <p class="mb-2 text-gray-600"><strong>Gender: </strong>{{ selectedQueueDetail.userGender }}</p>
                <p class="mb-2 text-gray-600"><strong>Birth Day: </strong>{{ selectedQueueDetail.userBirthday }}</p>
                <p class="mb-2 text-gray-600"><strong>Phone : </strong>{{ selectedQueueDetail.userPhone }}</p>
              </div>
              <!-- Thêm thông tin phần description -->
              <div>

              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>
</template>
<script setup lang="ts">
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { useQueueStore } from '@PKG_SRC/stores/Modules/DashBoard/PlannedCustomer/queueStore'

  const store = useQueueStore()
  const selectedQueueDetail = ref<any>()
  const rightPanelKey = ref(0)

  const handleSelect = (queueDetail: any) => {
    selectedQueueDetail.value = queueDetail
    rightPanelKey.value++
  }

  onMounted(async () => {
    await store.GetQueue('')
    if (store.queueListDetail.length > 0) {
      selectedQueueDetail.value = store.queueListDetail[0]
    }
  })
</script>
