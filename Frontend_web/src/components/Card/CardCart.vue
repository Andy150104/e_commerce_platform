<template>
  <div v-for="(cart, index) in cartList" :key="index"
    class="grid grid-cols-7 bg-white dark:bg-gray-900 rounded-lg p-4 w-full max-w-3xl shadow-md border border-gray-40000 dark:border-gray-700 mb-5">
    <div class="col-span-2 flex justify-center items-center">
      <Swiper class="relative w-20 h-20 md:w-36 md:h-36" :pagination="true" :spaceBetween="10" :slidesPerView="1"
        :loop="true">
        <SwiperSlide v-for="(image, imgIndex) in cart.images" :key="imgIndex">
          <img v-if="image.imageUrl" class="w-full h-full rounded-lg object-cover" :src="image.imageUrl"
            alt="Product Image" />
        </SwiperSlide>
      </Swiper>
    </div>

    <!-- Product Info -->
    <div class="col-span-2 md:col-span-3 px-4 flex flex-col justify-center">
      <h2 class="text-sm md:text-lg font-semibold text-gray-800 dark:text-gray-200">{{ cart.accessoryName }}</h2>
      <p class="text-xs md:text-sm text-gray-500 dark:text-gray-400">{{ cart.accessoryId }}</p>
      <span class="text-base md:text-lg font-semibold text-gray-800 dark:text-gray-200">{{ moneyFormatter(Number(cart.price)) }}</span>
      <span class="text-base md:text-lg font-semibold text-gray-800 dark:text-gray-200">Total price: {{ moneyFormatter(Number(cart.unitPrice)) }}</span>
    </div>
    <!-- Quantity & Delete -->
    <div class="col-span-2 flex flex-row justify-between items-center gap-2 md:gap-4">
      <!-- Quantity Control -->
      <BaseControlQuantityInput :xml-column="getXmlColumn(index)" :maxlength="2" :text-model="cart.quantity?.toString()"
        :disabled="false" @on-add-cart="onUpdate(index, cart.accessoryId ?? '')" />
      <!-- Delete Button -->
      <button class="text-gray-500 dark:text-gray-400 hover:text-red-500 dark:hover:text-red-400 text-base md:text-lg"
        type="button" @click="onDelete(index, cart.accessoryId ?? '')">
        ✖
      </button>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { Swiper, SwiperSlide } from 'swiper/vue'
  import 'swiper/css'
  import 'swiper/css/pagination'
  import { useCartStore } from '@PKG_SRC/stores/Modules/Blind_Box/CartStore'
  import { useForm } from 'vee-validate'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import BaseControlQuantityInput from '../Basecontrol/BaseControlQuantityInput.vue'
  import { Currency, Locale } from '@PKG_SRC/types/enums/constantFrontend'
  import type { DPSDeleteCartItemReponse, DPSSelectCartItem, DPSSelectCartItemEntity } from '@PKG_SRC/composables/Client/api/@types'

  const props = defineProps({
    cartModel: {
      type: Array as PropType<DPSSelectCartItem[]>,
      required: false,
    },
  })

const moneyFormatter = (money?: number) => {
  if (money) return formatMoney(money, Currency.VND, Locale.VI_VN)
}
const emit = defineEmits(['updateTotal'])
watch(totalPrice, (newTotal) => {
  emit('updateTotal', newTotal)
})
const store = useCartStore()
const cartList = computed(() => props.cartModel || [])
const initialFields: Record<string, string> = {}
cartList.value.forEach((cart, index) => {
  initialFields[`quantity_${index}`] = cart.quantity?.toString() || '1'
})
const formContext = useForm({ initialValues: initialFields })
store.SetFields(formContext)
const xmlColumns = {
  quantity: XmlLoadColumn({
    id: 'quantity',
    name: 'quantity',
    rules: '',
    visible: true,
    option: '',
  }),
}

const getXmlColumn = (index: number) => {
  return XmlLoadColumn({
    id: `quantity_${index}`,
    name: `quantity_${index}`,
    rules: '',
    visible: true,
    option: '',
  })
}

const onUpdate = async (index: number, productId: string) => {
  await store.UpdateCart(index, productId)
  await store.GetAllCart()
}

const onDelete = async (index: number, productId: string) => {
  await store.DeleteCart(index, productId)
}
</script>
