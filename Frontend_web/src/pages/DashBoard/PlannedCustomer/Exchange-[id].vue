<template>
  <BaseScreenDashBoard>
    <template #body>
      <div class="flex justify-center items-center h-full">
        <div class="container mx-auto px-4 py-8">
          <div class="grid grid-cols-1 gap-6 md:grid-cols-[1fr_2fr]">
            <!-- Danh sách Queue -->
            <div class="h-full space-y-4 overflow-y-auto flex flex-col justify-center items-center">
              <div v-if="store.queueListDetail.length === 0" class="text-gray-500 text-lg text-center">No queue available</div>

              <div
                v-for="queueDetail in store.queueListDetail"
                :key="queueDetail.queueId"
                @click="handleSelect(queueDetail)"
                class="relative rounded-lg border cursor-pointer p-4 shadow transition"
                :class="
                  selectedQueueDetail?.queueId === queueDetail.queueId ? 'border-2 border-sky-500 bg-sky-50' : 'border border-gray-200 bg-white'
                "
              >
                <h2 class="mb-2 text-lg text-gray-600 font-semibold">
                  {{ queueDetail.userFullNameQueue || 'N/A' }}
                </h2>

                <div class="mb-3 flex items-center space-x-2">
                  <span class="inline-block rounded bg-blue-200 px-2 py-1 text-sm text-gray-800">
                    {{ 'Gender: ' + (queueDetail.userGender || 'N/A') }}
                  </span>
                  <span class="inline-block rounded bg-yellow-200 px-2 py-1 text-sm text-gray-800">
                    {{ 'Birthday: ' + (queueDetail.userBirthday || 'N/A') }}
                  </span>
                  <span class="inline-block rounded bg-green-200 px-2 py-1 text-sm text-gray-800">
                    {{ 'Phone: ' + (queueDetail.userPhone || 'N/A') }}
                  </span>
                </div>

                <!-- Panel xuất hiện ngay dưới queue đã chọn (CHỈ trên mobile) -->
                <QueueDetailPanel
                  v-if="isMobile && selectedQueueDetail?.queueId === queueDetail.queueId"
                  :detail="selectedQueueDetail"
                  @close="selectedQueueDetail = null"
                  @approve="OnApproveQueue"
                />
              </div>
            </div>

            <!-- Panel Chi Tiết (Chỉ hiển thị trên desktop) -->
            <div
              v-if="selectedQueueDetail && !isMobile"
              :key="rightPanelKey"
              class="animate-fade-left animate-once animate-ease-in-out bg-white rounded-lg border border-gray-200 shadow p-6 md:sticky md:top-0 md:right-0 md:h-auto md:w-auto md:max-w-none w-full max-w-md mx-auto mt-4"
            >
              <div class="grid grid-cols-1 md:grid-cols-[auto_1fr_auto] gap-6 items-start">
                <!-- Ảnh đại diện -->
                <img
                  :src="
                    selectedQueueDetail.userImage ||
                    'https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ='
                  "
                  alt="Profile Picture"
                  class="w-32 h-32 md:w-48 md:h-48 rounded-full border mx-auto md:mx-0 object-cover"
                />

                <!-- Thông tin chi tiết -->
                <div class="space-y-2 text-center md:text-left">
                  <h2 class="text-2xl text-gray-600 font-semibold">{{ selectedQueueDetail.userFullNameQueue || 'N/A' }}</h2>
                  <p class="text-gray-600">Công ty HICAS</p>
                  <p class="text-gray-600"><strong>Gender: </strong>{{ selectedQueueDetail.userGender || 'N/A' }}</p>
                  <p class="text-gray-600"><strong>Birth Day: </strong>{{ selectedQueueDetail.userBirthday || 'N/A' }}</p>
                  <p class="text-gray-600"><strong>Phone: </strong>{{ selectedQueueDetail.userPhone || 'N/A' }}</p>
                </div>

                <button
                  @click="OnApproveQueue(selectedQueueDetail?.queueId)"
                  class="self-start rounded bg-red-500 px-4 py-2 font-medium text-white hover:bg-red-600 block"
                >
                  Approve Exchange
                </button>
              </div>

              <!-- Mô tả -->
              <div class="mt-6 p-4 rounded-lg border border-gray-200 bg-gray-50 w-full min-h-[160px]">
                <p class="text-gray-600">
                  <strong>Description: </strong>
                  <span v-html="selectedQueueDetail.descriptionQueue || 'No description available'"></span>
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>
</template>

<script setup lang="ts">
  import QueueDetailPanel from '@PKG_SRC/components/Modal/QueueDetailPanel.vue'
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { useQueueStore } from '@PKG_SRC/stores/Modules/DashBoard/PlannedCustomer/queueStore'
  import { ref, onMounted, onUnmounted } from 'vue'

  const store = useQueueStore()
  const selectedQueueDetail = ref<any>()
  const isMobilePanelVisible = ref(false)
  const rightPanelKey = ref(0)
  const route = useRoute()
  const isMobile = ref(window.innerWidth < 768) // Kiểm tra nếu là mobile

  const handleSelect = (queueDetail: any) => {
    selectedQueueDetail.value = queueDetail
    rightPanelKey.value++
    if (isMobile.value) {
      isMobilePanelVisible.value = true // Hiện panel trên mobile
    }
  }
  const checkScreenSize = () => {
    isMobile.value = window.innerWidth < 768
    if (!isMobile.value) {
      isMobilePanelVisible.value = true // Desktop luôn hiển thị
    } else {
      isMobilePanelVisible.value = false // Mobile mặc định ẩn
    }
  }
  const ExchangeId = computed(() => {
    const id = route.params.id
    return Array.isArray(id) ? id[0] : id
  })

  const OnApproveQueue = (QueueId: string) => {
    if (!ExchangeId.value || !QueueId) return
    store.ApproveQueue(ExchangeId.value, QueueId)
  }

  onMounted(async () => {
    checkScreenSize()
    window.addEventListener('resize', checkScreenSize)
    await store.GetQueueById(ExchangeId.value)
    if (store.queueListDetail.length > 0) {
      selectedQueueDetail.value = store.queueListDetail[0]
    }
  })
  onUnmounted(() => {
    window.removeEventListener('resize', checkScreenSize)
  })
</script>
