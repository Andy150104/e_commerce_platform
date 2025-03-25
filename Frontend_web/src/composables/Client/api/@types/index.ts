/* eslint-disable */
export type DPSInsertWishListResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type AbstractApiResponseOfString = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: string | null | undefined;
}

export type DetailError = {
  field?: string | undefined;
  messageId?: string | undefined;
  errorMessage?: string | undefined;
}

export type DPSInsertWishListRequest = AbstractApiRequest & {
  codeAccessory?: string | undefined;
}

export type UDSUpdateUserAddressResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type UDSUpdateUserAddressRequest = AbstractApiRequest & {
  addressId?: string | undefined;
  addressLine?: string | null | undefined;
  ward?: string | null | undefined;
  city?: string | null | undefined;
  province?: string | null | undefined;
  district?: string | null | undefined;
}

export type UDSDeleteUserAddressResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type UDSDeleteUserAddressRequest = AbstractApiRequest & {
  addressId: string;
}

export type UDSInsertUserAddressResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type UDSInsertUserAddressRequest = AbstractApiRequest & {
  addressLine: string;
  ward: string;
  city: string;
  province: string;
  district: string;
}

export type UDSSelectUserAddressResponse = AbstractApiResponseOfListOfUDSSelectUserAddressEntity & {
  response?: UDSSelectUserAddressEntity[] | undefined;
}

export type UDSSelectUserAddressEntity = {
  addressId?: string | undefined;
  firstName?: string | undefined;
  lastName?: string | undefined;
  addressLine?: string | null | undefined;
  ward?: string | null | undefined;
  city?: string | null | undefined;
  province?: string | null | undefined;
  district?: string | null | undefined;
}

export type AbstractApiResponseOfListOfUDSSelectUserAddressEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: UDSSelectUserAddressEntity[] | null | undefined;
}

export type UDSSelectUserProfileResponse = AbstractApiResponseOfUDSSelectUserProfileEntity & {
  response?: UDSSelectUserProfileEntity | undefined;
}

export type UDSSelectUserProfileEntity = {
  email?: string | null | undefined;
  firstName?: string | null | undefined;
  lastName?: string | null | undefined;
  phoneNumber?: string | null | undefined;
  imageUrl?: string | null | undefined;
  birthDate?: string | null | undefined;
  gender?: number | null | undefined;
}

export type AbstractApiResponseOfUDSSelectUserProfileEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: UDSSelectUserProfileEntity | null | undefined;
}

export type UDSSelectUserProfileRequest = AbstractApiRequest

export type UDSUpdateUserProfileReponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type UDSUpdateUserProfileRequest = AbstractApiRequest & {
  firstName?: string | null | undefined;
  lastName?: string | null | undefined;
  phoneNumber?: string | null | undefined;
  birthDay?: string | null | undefined;
  imageUrl?: string | null | undefined;
  gender?: number | null | undefined;
}

export type URSUserRegisterResponse = AbstractApiResponseOfObject & {
}

export type AbstractApiResponseOfObject = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
}

export type URSUserRegisterRequest = AbstractApiRequest & {
  phoneNumber?: string | null | undefined;
  birthDay?: string | null | undefined;
  imageUrl?: string | null | undefined;
  gender?: number | null | undefined;
  addressLine?: string | null | undefined;
  ward?: string | null | undefined;
  city?: string | null | undefined;
  province?: string | null | undefined;
  district?: string | null | undefined;
  key: string;
}

export type MomoOrderLogicReturnResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type MomoOrderLogicReturnRequest = AbstractApiRequest & {
  orderId: string;
  ghnOrderCode: string;
}

export type RROApproveRefundRequestOrderResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type RROApproveRefundRequestOrderRequest = AbstractApiRequest & {
  refundId: string;
  isApproved: boolean;
  rejectedReason?: string | null | undefined;
}

export type RRODeleteRefundRequestOrderResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type RRODeleteRefundRequestOrderRequest = AbstractApiRequest & {
  refundId?: string | undefined;
}

export type RROInsertRefundRequestOrderResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type RROInsertRefundRequestOrderRequest = AbstractApiRequest & {
  orderId: string;
  refundReason: string;
  imageUrl: string;
}

export type RROSelectRefundRequestOrderResponse = AbstractApiResponseOfRROSelectRefundRequestOrderEntity & {
  response?: RROSelectRefundRequestOrderEntity | undefined;
}

export type RROSelectRefundRequestOrderEntity = {
  refundId?: string | undefined;
  orderId?: string | undefined;
  userName?: string | undefined;
  refundAmount?: number | undefined;
  refundReason?: string | undefined;
  refundStatus?: number | undefined;
  paymentMethod?: number | undefined;
  updatedAt?: string | undefined;
  createdAt?: string | undefined;
  processedBy?: string | null | undefined;
  approvedAt?: string | null | undefined;
  rejectedReason?: string | null | undefined;
}

export type AbstractApiResponseOfRROSelectRefundRequestOrderEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: RROSelectRefundRequestOrderEntity | null | undefined;
}

export type RROSelectRefundRequestOrdersResponse = AbstractApiResponseOfListOfRROSelectRefundRequestOrdersEntity & {
  response?: RROSelectRefundRequestOrdersEntity[] | undefined;
}

export type RROSelectRefundRequestOrdersEntity = {
  refundId?: string | undefined;
  orderId?: string | undefined;
  userName?: string | undefined;
  refundAmount?: number | undefined;
  refundStatus?: number | undefined;
  paymentMethod?: number | undefined;
  createdAt?: string | undefined;
}

export type AbstractApiResponseOfListOfRROSelectRefundRequestOrdersEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: RROSelectRefundRequestOrdersEntity[] | null | undefined;
}

export type RROUpdateRefundRequestOrderResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type RROUpdateRefundRequestOrderRequest = AbstractApiRequest & {
  refundId: string;
  refundReason?: string | null | undefined;
  imageUrl?: string | null | undefined;
}

export type VEXSAddToQueueResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type VEXSAddToQueueRequest = AbstractApiRequest & {
  exchangeId: string;
  description: string;
}

export type VEXSApproveQueueResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type VEXSApproveQueueRequest = AbstractApiRequest & {
  exchangeId?: string | undefined;
  queueId?: string | undefined;
}

export type OPSBuyingPlanResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type OPSBuyingPlanRequest = AbstractApiRequest & {
  planId: string;
}

export type HSShowPlanResponse = AbstractApiResponseOfListOfHSShowPlanEntity & {
  response?: HSShowPlanEntity[] | undefined;
}

export type HSShowPlanEntity = {
  planId?: string | undefined;
  planName?: string | undefined;
  description?: string | null | undefined;
  price?: number | undefined;
  durationMonths?: number | undefined;
}

export type AbstractApiResponseOfListOfHSShowPlanEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: HSShowPlanEntity[] | null | undefined;
}

export type HSShowPlanRequest = AbstractApiRequest

export type OPSAddPlanRefundResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type OPSAddPlanRefundRequest = AbstractApiRequest & {
  orderPlanId: string;
  reason: string;
}

export type InsertOrderResponse = AbstractApiResponseOfInsertOrderResponseEntity & {
  response?: InsertOrderResponseEntity | undefined;
}

export type InsertOrderResponseEntity = {
  momo?: MomoResponse | undefined;
  ghnCode?: string | undefined;
}

export type MomoResponse = {
  paymentUrl?: string | undefined;
  qrCodeUrl?: string | undefined;
  deeplink?: string | undefined;
  deeplinkWebInApp?: string | undefined;
}

export type AbstractApiResponseOfInsertOrderResponseEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: InsertOrderResponseEntity | null | undefined;
}

export type InsertOrderRequest = AbstractApiRequest & {
  orderDetails?: TOSOrderDetailRequest[] | null | undefined;
  paymentMethod: number;
  platForm: number;
  addressId: string;
}

export type TOSOrderDetailRequest = {
  accessoryId: string;
  quantity: number;
}

export type PaymentOrderCallbackResponse = AbstractApiResponseOfMomoResponse & {
  response?: MomoResponse | undefined;
}

export type AbstractApiResponseOfMomoResponse = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: MomoResponse | null | undefined;
}

export type PaymentOrderCallbackRequest = AbstractApiRequest & {
  orderId: string;
  platform: number;
}

export type SelectOrderResponse = AbstractApiResponseOfSelectOrderEntity & {
  response?: SelectOrderEntity | undefined;
}

export type SelectOrderEntity = {
  orderId?: string | undefined;
  quantity?: number | undefined;
  totalPrice?: number | undefined;
  status?: number | undefined;
  createdAt?: string | null | undefined;
  createdBy?: string | null | undefined;
  updatedAt?: string | null | undefined;
  updatedBy?: string | null | undefined;
  orderDetails?: SelectOrderDetails[] | undefined;
}

export type SelectOrderDetails = {
  orderDetailId?: string | undefined;
  accessoryId?: string | undefined;
  accessoryName?: string | undefined;
  quantity?: number | undefined;
  originalPrice?: number | undefined;
  unitPrice?: number | undefined;
  discountPercent?: number | undefined;
  discountedPrice?: number | undefined;
}

export type AbstractApiResponseOfSelectOrderEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: SelectOrderEntity | null | undefined;
}

export type SelectOrdersResponse = AbstractApiResponseOfListOfSelectOrdersEntity & {
  response?: SelectOrdersEntity[] | undefined;
}

export type SelectOrdersEntity = {
  orderId?: string | undefined;
  quantity?: number | undefined;
  totalPrice?: number | undefined;
  status?: number | undefined;
  orderDetails?: TOSSelectOrderDetails[] | undefined;
}

export type TOSSelectOrderDetails = {
  orderDetailId?: string | undefined;
  accessoryId?: string | undefined;
  accessoryName?: string | undefined;
  quantity?: number | undefined;
  originalPrice?: number | undefined;
  unitPrice?: number | undefined;
  discountPercent?: number | undefined;
  discountedPrice?: number | undefined;
}

export type AbstractApiResponseOfListOfSelectOrdersEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: SelectOrdersEntity[] | null | undefined;
}

export type SelectOrdersRequest = AbstractApiRequest

export type TrackingGhnOrderResponse = AbstractApiResponseOfGhnOrderResponse & {
  response?: GhnOrderResponse | undefined;
}

export type GhnOrderResponse = {
  code?: number | undefined;
  message?: string | undefined;
  data?: GhnOrderData | undefined;
}

export type GhnOrderData = {
  returnName?: string | undefined;
  returnPhone?: string | undefined;
  returnAddress?: string | undefined;
  returnWardCode?: string | undefined;
  returnDistrictId?: number | undefined;
  returnLocation?: GhnLocation | undefined;
  fromName?: string | undefined;
  fromPhone?: string | undefined;
  fromAddress?: string | undefined;
  fromWardCode?: string | undefined;
  fromDistrictId?: number | undefined;
  fromLocation?: GhnLocation | undefined;
  toName?: string | undefined;
  toPhone?: string | undefined;
  toAddress?: string | undefined;
  toWardCode?: string | undefined;
  toDistrictId?: number | undefined;
  toLocation?: GhnLocation | undefined;
  weight?: number | undefined;
  length?: number | undefined;
  width?: number | undefined;
  height?: number | undefined;
  codAmount?: number | undefined;
  insuranceValue?: number | undefined;
  requiredNote?: string | undefined;
  content?: string | undefined;
  items?: GhnOrderItem[] | undefined;
  orderCode?: string | undefined;
  status?: string | undefined;
  leadtimeOrder?: GhnLeadTime | undefined;
}

export type GhnLocation = {
  lat?: number | undefined;
  long?: number | undefined;
  cellCode?: string | undefined;
  placeId?: string | undefined;
  trustLevel?: number | undefined;
  wardcode?: string | undefined;
  mapSource?: string | undefined;
}

export type GhnOrderItem = {
  name?: string | undefined;
  code?: string | undefined;
  quantity?: number | undefined;
  length?: number | undefined;
  width?: number | undefined;
  height?: number | undefined;
  weight?: number | undefined;
  itemOrderCode?: string | undefined;
}

export type GhnLeadTime = {
  fromEstimateDate?: string | undefined;
  toEstimateDate?: string | undefined;
}

export type AbstractApiResponseOfGhnOrderResponse = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: GhnOrderResponse | null | undefined;
}

export type TrackingGhnOrderRequest = AbstractApiRequest & {
  orderId: string;
}

export type AEPSAddExchangeAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type AEPSCheckExchangeRecheckRequestAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type AEPSCheckExchangeRecheckRequestAccessoryRequest = AbstractApiRequest & {
  requestId?: string | undefined;
  isAccepted?: boolean | undefined;
}

export type AEPSFinalAcceptExchangeAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type AEPSFinalAcceptExchangeAccessoryRequest = AbstractApiRequest & {
  exchangeId?: string | undefined;
  queueId?: string | undefined;
  isAccepted?: boolean | undefined;
}

export type AEPSGetByIdExchangeAccessoryResponse = AbstractApiResponseOfAEPSGetByIdExchangeAccessoryEntity & {
  response?: AEPSGetByIdExchangeAccessoryEntity | undefined;
}

export type AEPSGetByIdExchangeAccessoryEntity = {
  exchangeId?: string | undefined;
  exchangeName?: string | undefined;
  description?: string | undefined;
  status?: number | null | undefined;
  blindBoxId?: string | undefined;
  imageBlindBoxList?: AEPSGetByIdExchangeAccessoryImageList[] | undefined;
}

export type AEPSGetByIdExchangeAccessoryImageList = {
  imageId?: string | undefined;
  imageUrls?: string | undefined;
}

export type AbstractApiResponseOfAEPSGetByIdExchangeAccessoryEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: AEPSGetByIdExchangeAccessoryEntity | null | undefined;
}

export type AEPSGetExchangeAccessoryResponse = AbstractApiResponseOfListOfAEPSGetExchangeAccessoryEntity & {
  response?: AEPSGetExchangeAccessoryEntity[] | undefined;
}

export type AEPSGetExchangeAccessoryEntity = {
  exchangeId?: string | undefined;
  exchangeName?: string | undefined;
  description?: string | undefined;
  status?: number | null | undefined;
  blindBoxId?: string | undefined;
  imageBlindBoxList?: AEPSGetExchangeAccessoryImageBlindBoxList[] | undefined;
}

export type AEPSGetExchangeAccessoryImageBlindBoxList = {
  imageId?: string | undefined;
  imageUrls?: string | undefined;
}

export type AbstractApiResponseOfListOfAEPSGetExchangeAccessoryEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: AEPSGetExchangeAccessoryEntity[] | null | undefined;
}

export type AEPSGetExchangeAccessoryRequest = AbstractApiRequest

export type AEPSGetExchangeRecheckRequestAccessoryResponse = AbstractApiResponseOfListOfAEPSGetExchangeRecheckRequestAccessoryEntity & {
  response?: AEPSGetExchangeRecheckRequestAccessoryEntity[] | undefined;
}

export type AEPSGetExchangeRecheckRequestAccessoryEntity = {
  requestId?: string | undefined;
  exchangeId?: string | undefined;
  description?: string | undefined;
  status?: number | undefined;
  isActive?: boolean | null | undefined;
  createdAt?: string | null | undefined;
  updatedAt?: string | null | undefined;
  createdBy?: string | null | undefined;
  updatedBy?: string | null | undefined;
}

export type AbstractApiResponseOfListOfAEPSGetExchangeRecheckRequestAccessoryEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: AEPSGetExchangeRecheckRequestAccessoryEntity[] | null | undefined;
}

export type AEPSGetExchangeRecheckRequestAccessoryRequest = AbstractApiRequest

export type AEPSGetFailExchangeAccessoryResponse = AbstractApiResponseOfListOfAEPSGetFailExchangeAccessoryEntity & {
  response?: AEPSGetFailExchangeAccessoryEntity[] | undefined;
}

export type AEPSGetFailExchangeAccessoryEntity = {
  exchangeId?: string | undefined;
  exchangeName?: string | undefined;
  description?: string | undefined;
  status?: number | null | undefined;
  blindBoxId?: string | undefined;
}

export type AbstractApiResponseOfListOfAEPSGetFailExchangeAccessoryEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: AEPSGetFailExchangeAccessoryEntity[] | null | undefined;
}

export type AEPSGetFailExchangeAccessoryRequest = AbstractApiRequest

export type AEPSSendExchangeRecheckRequestAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type AEPSSendExchangeRecheckRequestAccessoryRequest = AbstractApiRequest & {
  exchangeId?: string | undefined;
  description?: string | null | undefined;
}

export type VEXSGetOrderExchangeResponse = AbstractApiResponseOfListOfVEXSGetOrderExchangeResponseEntity & {
  response?: VEXSGetOrderExchangeResponseEntity[] | undefined;
}

export type VEXSGetOrderExchangeResponseEntity = {
  exchangeId?: string | undefined;
  exchangeName?: string | undefined;
}

export type AbstractApiResponseOfListOfVEXSGetOrderExchangeResponseEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: VEXSGetOrderExchangeResponseEntity[] | null | undefined;
}

export type VEXSGetOrderExchangeRequest = AbstractApiRequest

export type VEXSGetToQueueResponse = AbstractApiResponseOfListOfVEXSGetToQueueResponseEntity & {
  response?: VEXSGetToQueueResponseEntity[] | undefined;
}

export type VEXSGetToQueueResponseEntity = {
  queueId?: string | undefined;
  userFullNameQueue?: string | undefined;
  userImage?: string | undefined;
  descriptionQueue?: string | undefined;
  userGender?: string | undefined;
  userPhone?: string | undefined;
  userBirthday?: string | undefined;
  status?: number | null | undefined;
}

export type AbstractApiResponseOfListOfVEXSGetToQueueResponseEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: VEXSGetToQueueResponseEntity[] | null | undefined;
}

export type DashboardResponse = AbstractApiResponseOfDashboardEntity & {
  response?: DashboardEntity | undefined;
}

export type DashboardEntity = {
  totalRevenue?: number | undefined;
  totalRevenueThisMonth?: number | undefined;
  revenueGrowthRateLastMonth?: number | undefined;
  totalRevenueToday?: number | undefined;
  revenueGrowthRateYesterday?: number | undefined;
  totalOrder?: number | undefined;
  revenueData?: number[] | undefined;
  accessoryData?: AccessoryDataResponse[] | undefined;
}

export type AccessoryDataResponse = {
  accessoryName?: string | undefined;
  totalSale?: number | undefined;
}

export type AbstractApiResponseOfDashboardEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: DashboardEntity | null | undefined;
}

export type CreateCategoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type CreateCategoryRequest = AbstractApiRequest & {
  categoryName: string;
  parentId?: string | null | undefined;
}

export type DeleteCategoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type DeleteCategoryRequest = AbstractApiRequest & {
  categoryId?: string | null | undefined;
}

export type SelectCategoriesResponse = AbstractApiResponseOfListOfSelectCategoriesEntity & {
  response?: SelectCategoriesEntity[] | undefined;
}

export type SelectCategoriesEntity = {
  categoryId?: string | undefined;
  categoryName?: string | undefined;
  parentId?: string | null | undefined;
}

export type AbstractApiResponseOfListOfSelectCategoriesEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: SelectCategoriesEntity[] | null | undefined;
}

export type SelectCategoriesRequest = AbstractApiRequest

export type SelectCategoryResponse = AbstractApiResponseOfSelectCategoryEntity & {
  response?: SelectCategoryEntity | undefined;
}

export type SelectCategoryEntity = {
  categoryId?: string | undefined;
  categoryName?: string | undefined;
  parentId?: string | null | undefined;
}

export type AbstractApiResponseOfSelectCategoryEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: SelectCategoryEntity | null | undefined;
}

export type SelectSubCategoriesResponse = AbstractApiResponseOfListOfSelectSubCategoriesEntity & {
  response?: SelectSubCategoriesEntity[] | undefined;
}

export type SelectSubCategoriesEntity = {
  categoryId?: string | undefined;
  categoryName?: string | undefined;
}

export type AbstractApiResponseOfListOfSelectSubCategoriesEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: SelectSubCategoriesEntity[] | null | undefined;
}

export type UpdateCategoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type UpdateCategoryRequest = AbstractApiRequest & {
  categoryId?: string | undefined;
  categoryName?: string | undefined;
  parentId?: string | null | undefined;
}

export type DPSDeleteCartItemReponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type DPSDeleteCartItemRequest = AbstractApiRequest & {
  codeAccessory?: string | undefined;
}

export type DPSSelectCartItemResponse = AbstractApiResponseOfDPSSelectCartItemEntity & {
  response?: DPSSelectCartItemEntity | undefined;
}

export type DPSSelectCartItemEntity = {
  totalPrice?: number | undefined;
  items?: DPSSelectCartItem[] | undefined;
}

export type DPSSelectCartItem = {
  accessoryId?: string | undefined;
  accessoryName?: string | undefined;
  price?: number | undefined;
  quantity?: number | undefined;
  unitPrice?: number | null | undefined;
  shortDescription?: string | undefined;
  images?: DPSSelectCartItemImages[] | undefined;
}

export type DPSSelectCartItemImages = {
  imageUrl?: string | undefined;
}

export type AbstractApiResponseOfDPSSelectCartItemEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: DPSSelectCartItemEntity | null | undefined;
}

export type DPSSelectCartItemRequest = AbstractApiRequest

export type DPSUpdateCartItemResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type DPSUpdateCartItemRequest = AbstractApiRequest & {
  quantity?: number | undefined;
  accessoryId?: string | undefined;
}

export type MPSSelectAccessoriesResponse = AbstractApiResponseOfListOfMPSSelectAccessoriesEntity & {
  response?: MPSSelectAccessoriesEntity[] | undefined;
}

export type MPSSelectAccessoriesEntity = {
  accessoryId?: string | undefined;
  accessoryName?: string | undefined;
  description?: string | null | undefined;
  shortDescription?: string | null | undefined;
  price?: number | undefined;
  discount?: number | null | undefined;
  quantity?: number | undefined;
  createdAt?: string | null | undefined;
  childCategoryName?: string | null | undefined;
  parentCategoryName?: string | null | undefined;
  averageRating?: number | null | undefined;
  totalReviews?: number | null | undefined;
  totalSold?: number | null | undefined;
  totalOrders?: number | null | undefined;
  imageAccessoriesList?: MPSSelectImageAccessories[] | undefined;
}

export type MPSSelectImageAccessories = {
  imageId?: string | undefined;
  accessoryId?: string | undefined;
  imageUrl?: string | undefined;
}

export type AbstractApiResponseOfListOfMPSSelectAccessoriesEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: MPSSelectAccessoriesEntity[] | null | undefined;
}

export type MPSSelectAccessoriesRequest = AbstractApiRequest

export type DPSInsertCartResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type DPSInsertCartRequest = AbstractApiRequest & {
  codeAccessory: string;
  quantity?: number | undefined;
}

export type DPSDeleteWishListResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type DPSDeleteWishListRequest = AbstractApiRequest & {
  codeAccessory?: string | undefined;
}

export type DPSSelectWishListResponse = AbstractApiResponseOfListOfDPSSelectWishListEntity & {
  response?: DPSSelectWishListEntity[] | undefined;
}

export type DPSSelectWishListEntity = {
  accessoryId?: string | undefined;
  accessoryName?: string | undefined;
  shortDescription?: string | null | undefined;
  images?: DPSSelectWishListImages[] | undefined;
}

export type DPSSelectWishListImages = {
  imageUrl?: string | undefined;
}

export type AbstractApiResponseOfListOfDPSSelectWishListEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;
  response?: DPSSelectWishListEntity[] | null | undefined;
}

export type DPSSelectWishListRequest = AbstractApiRequest

export type DPSSelectAccessoryResponse = AbstractApiResponseOfDPSSelectAccessoryEntity & {
  response?: DPSSelectAccessoryEntity | undefined;
}

export type DPSSelectAccessoryEntity = {
  codeAccessory?: string | undefined;
  nameAccessory?: string | undefined;
  shortDescription?: string | null | undefined;
  description?: string | null | undefined;
  price?: string | undefined;
  discount?: string | undefined;
  salePrice?: string | null | undefined;
  createdAt?: string | null | undefined;
  childCategoryName?: string | null | undefined;
  parentCategoryName?: string | null | undefined;
  wishlistId?: string | null | undefined;
  imageUrls?: DpsSelectAccessoryListImageUrl[] | undefined;
  averageRating?: number | null | undefined;
  totalReviews?: number | null | undefined;
}

export type DpsSelectAccessoryListImageUrl = {
  imageUrl?: string | undefined;
}

export type AbstractApiResponseOfDPSSelectAccessoryEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: DPSSelectAccessoryEntity | null | undefined;
}

export type DPSSelectItemResponse = AbstractApiResponseOfDPSSelectItemEntity & {
  response?: DPSSelectItemEntity | undefined;
}

export type DPSSelectItemEntity = {
  totalRecords?: number | undefined;
  totalPages?: number | undefined;
  items?: ItemEntity[] | undefined;
}

export type ItemEntity = {
  codeProduct?: string | undefined;
  nameAccessory?: string | undefined;
  exchangeName?: string | undefined;
  shortDescription?: string | null | undefined;
  description?: string | null | undefined;
  price?: string | undefined;
  discount?: string | undefined;
  salePrice?: string | undefined;
  createdAt?: string | null | undefined;
  childCategoryName?: string | null | undefined;
  parentCategoryName?: string | null | undefined;
  wishListId?: string | null | undefined;
  imageUrl?: DpsSelectItemListImageUrl[] | undefined;
  averageRating?: number | null | undefined;
  totalReviews?: number | null | undefined;
  exchangeId?: string | null | undefined;
  firstNameCreator?: string | undefined;
  lastNameCreator?: string | undefined;
}

export type DpsSelectItemListImageUrl = {
  imageUrl?: string | undefined;
}

export type AbstractApiResponseOfDPSSelectItemEntity = {
  success?: boolean | undefined;
  messageId?: string | undefined;
  message?: string | undefined;
  detailErrorList?: DetailError[] | undefined;

  response?: DPSSelectItemEntity | null | undefined;
}

export type MPSDeleteAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type MPSDeleteAccessoryRequest = AbstractApiRequest & {
  codeAccessory?: string[] | undefined;
}

export type MPSDeleteImageAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type MPSDeleteImageAccessoryRequest = AbstractApiRequest & {
  imageId: string[];
}

export type MPSInsertAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type MPSUpdateAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}

export type MSPInsertImageAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined;
}
export type AbstractApiRequest = {
  
}