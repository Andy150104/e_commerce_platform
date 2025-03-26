<template>
  <BaseScreenDashBoard>
    <template #body>
      <div>
        <!-- Vùng container chính -->
        <div class="container mx-auto px-4 py-8">
          <!-- Grid chia 2 cột trên desktop, 1 cột trên mobile -->
          <div class="grid grid-cols-1 gap-6 md:grid-cols-[1fr_2fr]">
            <!-- CỘT TRÁI: Danh sách Queue -->
            <div class="h-[80vh] space-y-4 overflow-y-auto">
              <!-- Trường hợp không có hàng đợi -->
              <div v-if="store.queueListDetail.length === 0" class="text-gray-500 text-lg text-center">No queue available</div>
              <div
                v-for="queueDetail in store.queueListDetail"
                :key="queueDetail.queueId"
                @click="handleSelect(queueDetail)"
                class="animate-fade-right animate-once animate-ease-in-out relative rounded-lg border p-4 shadow transition"
                :class="[
                  selectedQueueDetail?.queueId === queueDetail.queueId ? 'border-2 border-sky-500 bg-sky-50' : 'border border-gray-200 bg-white',
                  (queueDetail.status === 2 || !hasQueueStatus2) && queueDetail.status !== 3 ? 'cursor-pointer' : 'opacity-50 cursor-not-allowed',
                ]"
              >
                <!-- Nhãn trạng thái ở góc trên bên phải -->
                <span
                  class="absolute top-4 right-4 rounded px-2 py-1 text-xs font-semibold text-white"
                  :class="{
                    'bg-yellow-500': queueDetail.status === 1,
                    'bg-blue-500': queueDetail.status === 2,
                    'bg-red-500': queueDetail.status === 3,
                  }"
                >
                  {{ queueDetail.status === 1 ? 'Waiting' : queueDetail.status === 2 ? 'Changing' : 'Fail' }}
                </span>
                <h2 class="mb-2 text-lg font-semibold text-gray-600">
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
                <div
                  v-if="isMobile && selectedQueueDetail?.queueId === queueDetail.queueId"
                  class="animate-fade-left animate-once animate-ease-in-out block md:hidden mt-4 rounded-lg border border-gray-200 bg-white p-4 shadow"
                >
                  <img
                    :src="
                      selectedQueueDetail?.userImage && selectedQueueDetail.userImage.trim() !== ''
                        ? selectedQueueDetail.userImage
                        : 'https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ='
                    "
                    alt="Profile Picture"
                    class="w-32 h-32 md:w-48 md:h-48 rounded-full border mx-auto md:mx-0 object-cover"
                  />
                  <h2 class="mb-2 text-xl font-semibold">
                    {{ selectedQueueDetail.userFullNameQueue }}
                  </h2>
                  <div class="mt-4">
                    <p class="mb-2 text-gray-600"><strong>Gender: </strong>{{ selectedQueueDetail.userGender }}</p>
                    <p class="mb-2 text-gray-600"><strong>Birth Day: </strong>{{ selectedQueueDetail.userBirthday }}</p>
                    <p class="mb-2 text-gray-600"><strong>Phone: </strong>{{ selectedQueueDetail.userPhone }}</p>
                  </div>
                  <button
                    v-if="selectedQueueDetail?.status !== 2 && selectedQueueDetail?.status !== 3"
                    @click.stop="OnApproveQueue(selectedQueueDetail?.queueId)"
                    class="mt-4 rounded bg-red-500 px-4 py-2 font-medium text-white hover:bg-red-600"
                  >
                    Approve Exchange
                  </button>
                  <div v-else-if="selectedQueueDetail?.status === 2 && updatedQueueDetail?.status !== 3" class="mt-4 flex space-x-2">
                    <button @click.stop="FinaleApproveQueue(true)" class="rounded bg-green-500 px-4 py-2 font-medium text-white hover:bg-green-600">
                      Accept
                    </button>
                    <button @click.stop="FinaleApproveQueue(false)" class="rounded bg-gray-500 px-4 py-2 font-medium text-white hover:bg-gray-600">
                      Reject
                    </button>
                  </div>
                  <div v-else-if="updatedQueueDetail?.status === 3" class="mt-4 text-green-500 font-semibold">Exchange Completed</div>

                  <!-- Mô tả -->
                  <div class="mt-4 p-4 rounded-lg border border-gray-200 bg-gray-50 w-full">
                    <div class="text-gray-600 h-3/5 overflow-y-auto">
                      <h1>Description:</h1>
                      <div class="custom">
                        <div v-html="selectedQueueDetail.descriptionQueue || 'No description available'"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- CỘT PHẢI: Panel Chi Tiết (hiển thị trên desktop) -->
            <div
              v-if="selectedQueueDetail && !isMobile"
              :key="rightPanelKey"
              class="animate-fade-left animate-once animate-ease-in-out hidden md:block sticky h-full overflow-auto top-0 rounded-lg border border-gray-200 bg-white p-6 shadow"
            >
              <!-- Header: Tên + Nút duyệt -->
              <div class="mb-6 flex items-start justify-between">
                <div>
                  <div>
                    <h2 class="mb-2 text-2xl font-semibold">{{ selectedQueueDetail.userFullNameQueue || 'N/A' }}</h2>
                  </div>
                  <img
                    :src="
                      selectedQueueDetail?.userImage && selectedQueueDetail.userImage.trim() !== ''
                        ? selectedQueueDetail.userImage
                        : 'https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ='
                    "
                    alt="Profile Picture"
                    class="w-32 h-32 md:w-48 md:h-48 rounded-full border mx-auto md:mx-0 object-cover"
                  />
                </div>
                <!-- Nút Approve / Accept / Reject / Completed -->
                <button
                  v-if="selectedQueueDetail?.status !== 2 && selectedQueueDetail?.status !== 3"
                  @click="OnApproveQueue(selectedQueueDetail?.queueId)"
                  class="rounded bg-red-500 px-4 py-2 font-medium text-white hover:bg-red-600"
                >
                  Approve Exchange
                </button>
                <div v-else-if="selectedQueueDetail?.status === 2 && updatedQueueDetail?.status !== 3" class="space-x-2">
                  <ModelFullScreen class="ml-3 mb-4">
                    <template #body>
                      <ChatGateWay :user-name="userStore.UserInfo.usrId" :receiver-id="selectedQueueDetail.userFullNameQueue" />
                    </template>
                  </ModelFullScreen>
                  <button @click="FinaleApproveQueue(true)" class="rounded bg-green-500 px-4 py-2 font-medium text-white hover:bg-green-600">
                    Accept
                  </button>
                  <button @click="FinaleApproveQueue(false)" class="rounded bg-gray-500 px-4 py-2 font-medium text-white hover:bg-gray-600">
                    Reject
                  </button>
                </div>
                <div v-else-if="updatedQueueDetail?.status === 3" class="text-green-500 font-semibold">Exchange Completed</div>
              </div>

              <!-- Thông tin cơ bản -->
              <div class="mb-6">
                <p class="mb-2 text-gray-600"><strong>Gender: </strong>{{ selectedQueueDetail.userGender || 'N/A' }}</p>
                <p class="mb-2 text-gray-600"><strong>Birth Day: </strong>{{ selectedQueueDetail.userBirthday || 'N/A' }}</p>
                <p class="mb-2 text-gray-600"><strong>Phone: </strong>{{ selectedQueueDetail.userPhone || 'N/A' }}</p>
              </div>

              <!-- Mô tả -->
              <div class="p-4 rounded-lg border border-gray-200 bg-gray-50 w-full">
                <div class="text-gray-600 h-[50vh] overflow-y-auto">
                  <h1>Description:</h1>
                  <div class="custom">
                    <div v-html="selectedQueueDetail.descriptionQueue || 'No description available'"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Modal Lỗi Hệ Thống -->
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

        <!-- Modal Hoàn Tất Exchange -->
        <Teleport to="body">
          <div v-if="showExchangeCompletedModal" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
            <div class="bg-white rounded-lg p-6 w-96 shadow-lg text-center">
              <h2 class="text-xl font-semibold text-green-600">Exchange has been completed</h2>
              <p class="text-gray-700 mt-4">You can go back to the planned customer page.</p>
              <p class="text-gray-700 mt-4">If this exchange is still happening, please let us know!</p>
              <router-link to="/DashBoard/PlannedCustomer" class="mt-4 inline-block px-6 py-3 bg-blue-500 text-white rounded hover:bg-blue-600">
                Go to My Exchange
              </router-link>
            </div>
          </div>
        </Teleport>
      </div>
    </template>
  </BaseScreenDashBoard>
</template>

<script setup lang="ts">
  import ChatGateWay from '@PKG_SRC/components/Chat/ChatGateWay.vue'
  import ModelFullScreen from '@PKG_SRC/components/Modal/ModelFullScreen.vue'
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { useUserStore } from '@PKG_SRC/stores/master/userStore'
  import { useExchangeStore } from '@PKG_SRC/stores/Modules/Blind_Box/ExchangeStore'
  import { useQueueStore } from '@PKG_SRC/stores/Modules/DashBoard/PlannedCustomer/queueStore'
  import { ref, onMounted, onUnmounted } from 'vue'

  const store = useQueueStore()
  const exchangeStore = useExchangeStore()
  const selectedQueueDetail = ref<any>()
  const isMobilePanelVisible = ref(false)
  const userStore = useUserStore()
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
      await store.ApproveQueue(ExchangeId.value, QueueId)
      await store.GetQueueById(ExchangeId.value)
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
    await store.FinaleApproveQueue(ExchangeId.value, selectedQueueDetail.value.queueId, isAccepted)
    if (isAccepted) {
      updatedQueueDetail.value = { ...selectedQueueDetail.value, status: 3 }
    }
    store.queueListDetail = store.queueListDetail.map((queue) => {
      if (queue.queueId === selectedQueueDetail.value?.queueId) {
        return { ...queue, status: isAccepted ? 2 : 3 }
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
    store.ResetStore()
  })
</script>
<style scoped>
  :deep(.custom h1) {
    @apply text-3xl font-bold my-4;
  }
  :deep(.custom h2) {
    @apply text-2xl font-semibold my-3;
  }
  :deep(.custom h3) {
    @apply text-xl font-medium my-2;
  }
</style>
