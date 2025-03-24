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
                class="relative rounded-lg border p-4 shadow transition flex justify-between items-center"
                :class="[
                  selectedQueueDetail?.queueId === queueDetail.queueId ? 'border-2 border-sky-500 bg-sky-50' : 'border border-gray-200 bg-white',
                  (queueDetail.status === 2 || !hasQueueStatus2) && queueDetail.status !== 3 ? 'cursor-pointer' : 'opacity-50 cursor-not-allowed',
                ]"
              >
                <div>
                  <h2 class="mb-2 text-lg text-gray-600 font-semibold flex items-center">
                    {{ queueDetail.userFullNameQueue || 'N/A' }}
                    <span
                      class="ml-2 text-sm font-medium px-2 py-1 rounded-full"
                      :class="{
                        'bg-yellow-500 text-white': queueDetail.status === 1,
                        'bg-blue-500 text-white': queueDetail.status === 2,
                        'bg-red-500 text-white': queueDetail.status === 3,
                      }"
                    >
                      {{ queueDetail.status === 1 ? 'Waiting' : queueDetail.status === 2 ? 'Changing' : 'Fail' }}
                    </span>
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

                <!-- Nút Approve Exchange (chỉ hiển thị khi status không phải 2) -->
                <button
                  v-if="selectedQueueDetail?.status !== 2 && selectedQueueDetail?.status !== 3"
                  @click="OnApproveQueue(selectedQueueDetail?.queueId)"
                  class="self-start rounded bg-red-500 px-4 py-2 font-medium text-white hover:bg-red-600 block"
                >
                  Approve Exchange
                </button>

                <div v-if="selectedQueueDetail?.status === 2 && updatedQueueDetail?.status !== 3">
                  <button @click="FinaleApproveQueue(true)" class="rounded bg-green-500 px-4 py-2 font-medium text-white hover:bg-green-600">
                    Accept
                  </button>

                  <button @click="FinaleApproveQueue(false)" class="rounded bg-gray-500 px-4 py-2 font-medium text-white hover:bg-gray-600">
                    Reject
                  </button>
                </div>

                <div v-if="updatedQueueDetail?.status === 3" class="text-green-500 font-semibold">Exchange Completed</div>
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
      <Teleport to="body">
        <div v-if="showErrorModal" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
          <div class="bg-white rounded-lg p-6 w-96 shadow-lg text-center">
            <h2 class="text-xl font-semibold text-red-600">Lỗi hệ thống</h2>
            <p class="text-gray-700 mt-2">{{ errorMessage }}</p>
            <p class="text-gray-600 mt-2">Vui lòng liên hệ CSKH để được hỗ trợ.</p>
            <button @click="showErrorModal = false" class="mt-4 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600">Đóng</button>
          </div>
        </div>
      </Teleport>
      <Teleport to="body">
        <div v-if="showExchangeCompletedModal" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
          <div class="bg-white rounded-lg p-6 w-96 shadow-lg text-center">
            <h2 class="text-xl font-semibold text-green-600">Exchange has been completed</h2>
            <p class="text-gray-700 mt-4">You can go back to the planned customer page.</p>
            <p class="text-gray-700 mt-4">If this exchange is still happening, please let us know!</p>
            <a
              href="http://localhost:3000/DashBoard/PlannedCustomer"
              class="mt-4 inline-block px-6 py-3 bg-blue-500 text-white rounded hover:bg-blue-600"
            >
              Go to My Exchange
            </a>
          </div>
        </div>
      </Teleport>
    </template>
  </BaseScreenDashBoard>
</template>

<script setup lang="ts">
  import QueueDetailPanel from '@PKG_SRC/components/Modal/QueueDetailPanel.vue'
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { useExchangeStore } from '@PKG_SRC/stores/Modules/Blind_Box/ExchangeStore'
  import { useQueueStore } from '@PKG_SRC/stores/Modules/DashBoard/PlannedCustomer/queueStore'
  import { ref, onMounted, onUnmounted } from 'vue'

  const store = useQueueStore()
  const exchangeStore = useExchangeStore()
  const selectedQueueDetail = ref<any>()
  const isMobilePanelVisible = ref(false)
  const rightPanelKey = ref(0)
  const route = useRoute()
  const isMobile = ref(window.innerWidth < 768) // Kiểm tra nếu là mobile
  const hasQueueStatus2 = computed(() => store.queueListDetail.some((q) => q.status === 2))
  const showErrorModal = ref(false)
  const errorMessage = ref('')
  const showExchangeCompletedModal = ref(false)
  const updatedQueueDetail = ref<any>(null)

  const handleSelect = (queueDetail: any) => {
    if ((queueDetail.status === 2 || !hasQueueStatus2.value) && queueDetail.status !== 3) {
      selectedQueueDetail.value = queueDetail
      rightPanelKey.value++
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

  const OnApproveQueue = async (QueueId: string) => {
    if (!ExchangeId.value || !QueueId) return

    try {
      await store.ApproveQueue(ExchangeId.value, QueueId) // Gửi request duyệt
      await store.GetQueueById(ExchangeId.value) // Fetch lại danh sách queue

      // Giữ nguyên queue đang chọn nếu nó vẫn tồn tại sau khi fetch
      const updatedQueue = store.queueListDetail.find((q) => q.queueId === selectedQueueDetail.value?.queueId)
      if (updatedQueue) {
        selectedQueueDetail.value = updatedQueue // Cập nhật lại trạng thái
      } else {
        selectedQueueDetail.value = null // Nếu hàng đợi bị xóa, bỏ chọn
      }
    } catch (error) {
      console.error('Lỗi khi phê duyệt hàng đợi:', error)
    }
  }

  const FinaleApproveQueue = async (isAccepted: boolean) => {
    if (!ExchangeId.value || !selectedQueueDetail.value?.queueId) return

    console.log('Exchangeid:', ExchangeId.value, 'QueueId:', selectedQueueDetail.value?.queueId, 'isAccepted:', isAccepted)

    // Gọi API nhưng **éo tin nó**
    await store.FinaleApproveQueue(ExchangeId.value, selectedQueueDetail.value.queueId, isAccepted)

    console.log('✅ API gọi xong, tự cập nhật UI...')
    if (isAccepted) {
      updatedQueueDetail.value = { ...selectedQueueDetail.value, status: 3 }
    }
    // **Tìm queue bị cập nhật và đổi status**
    store.queueListDetail = store.queueListDetail.map((queue) => {
      if (queue.queueId === selectedQueueDetail.value?.queueId) {
        return { ...queue, status: isAccepted ? 2 : 3 } // Nếu reject thì set status = 3
      }
      return queue
    })

    // **Ép Vue cập nhật UI**
    store.queueListDetail = [...store.queueListDetail]

    // **Cập nhật lại queue đang chọn**
    selectedQueueDetail.value = null
    setTimeout(() => {
      selectedQueueDetail.value = store.queueListDetail.find((q) => q.status === 2) || store.queueListDetail[0] || null
      console.log('🔄 Queue sau khi cập nhật:', store.queueListDetail)
    }, 0)
  }

  watch(
    () => store.queueListDetail,
    (newVal) => {
      console.log('🔄 UI nhận diện thay đổi queueListDetail:', newVal)
    },
    { deep: true }
  )

  onMounted(async () => {
    await exchangeStore.GetByExchangeID(ExchangeId.value)
    // Kiểm tra trạng thái của exchangeDetails để hiển thị modal thông báo
    if (exchangeStore.exchangeDetails?.status === 3 || exchangeStore.exchangeDetails?.status === 4) {
      showExchangeCompletedModal.value = true
    }
    checkScreenSize()
    window.addEventListener('resize', checkScreenSize)
    await store.GetQueueById(ExchangeId.value)
   

    const status2Queues = store.queueListDetail.filter((q) => q.status === 2)
    if (status2Queues.length > 1) {
      errorMessage.value = 'Hệ thống phát hiện lỗi dữ liệu. Vui lòng liên hệ CSKH để được hỗ trợ.'
      showErrorModal.value = true
      return
    }

    selectedQueueDetail.value = status2Queues[0] || store.queueListDetail[0] || null
  })

  onUnmounted(() => {
    window.removeEventListener('resize', checkScreenSize)
  })
</script>
