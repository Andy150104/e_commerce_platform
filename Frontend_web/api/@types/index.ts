/* eslint-disable */
export type VEXSAddToQueueResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type AbstractApiResponseOfString = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined
  response?: string | null | undefined
}

export type DetailError = {
  field?: string | undefined
  messageId?: string | undefined
  errorMessage?: string | undefined
}

export type VEXSAddToQueueRequest = AbstractApiRequest & {
  blindBoxId: string
  description: string
}

export type AbstractApiRequest = {
  isOnlyValidation?: boolean | undefined
}

export type URSUserRegisterResponse = AbstractApiResponseOfObject & {}

export type AbstractApiResponseOfObject = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined
}

export type URSUserRegisterRequest = AbstractApiRequest & {
  phoneNumber?: string | null | undefined
  birthDay?: string | null | undefined
  imageUrl?: string | null | undefined
  gender?: number | null | undefined
  addressLine?: string | null | undefined
  ward?: string | null | undefined
  city?: string | null | undefined
  province?: string | null | undefined
  district?: string | null | undefined
  key: string
}

export type UDSInsertUserAddressResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type UDSInsertUserAddressRequest = AbstractApiRequest & {
  addressLine: string
  ward: string
  city: string
  province: string
  district: string
}

export type UDSSelectUserAddressResponse = AbstractApiResponseOfListOfUDSSelectUserAddressEntity & {
  response?: UDSSelectUserAddressEntity[] | undefined
}

export type UDSSelectUserAddressEntity = {
  addressId?: string | undefined
  firstName?: string | undefined
  lastName?: string | undefined
  addressLine?: string | null | undefined
  ward?: string | null | undefined
  city?: string | null | undefined
  province?: string | null | undefined
  district?: string | null | undefined
}

export type AbstractApiResponseOfListOfUDSSelectUserAddressEntity = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined
  response?: UDSSelectUserAddressEntity[] | null | undefined
}

export type UDSSelectUserAddressRequest = AbstractApiRequest

export type UDSSelectUserProfileResponse = AbstractApiResponseOfUDSSelectUserProfileEntity & {
  response?: UDSSelectUserProfileEntity | undefined
}

export type UDSSelectUserProfileEntity = {
  email?: string | null | undefined
  firstName?: string | null | undefined
  lastName?: string | null | undefined
  phoneNumber?: string | null | undefined
  imageUrl?: string | null | undefined
  birthDate?: string | null | undefined
  gender?: number | null | undefined
}

export type AbstractApiResponseOfUDSSelectUserProfileEntity = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined

  response?: UDSSelectUserProfileEntity | null | undefined
}

export type UDSSelectUserProfileRequest = AbstractApiRequest

export type UDSUpdateUserAddressResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type UDSUpdateUserAddressRequest = AbstractApiRequest & {
  addressId?: string | undefined
  addressLine?: string | null | undefined
  ward?: string | null | undefined
  city?: string | null | undefined
  province?: string | null | undefined
  district?: string | null | undefined
}

export type UDSUpdateUserProfileReponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type UDSUpdateUserProfileRequest = AbstractApiRequest & {
  firstName?: string | undefined
  lastName?: string | undefined
  phoneNumber?: string | null | undefined
  birthDay?: string | null | undefined
  imageUrl?: string | null | undefined
  gender?: number | null | undefined
}

export type OPSAddPlanRefundResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type OPSAddPlanRefundRequest = AbstractApiRequest & {
  orderPlanId: string
  reason: string
}

export type OPSBuyingPlanResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type OPSBuyingPlanRequest = AbstractApiRequest & {
  planId: string
  voucherId?: string | null | undefined
}

export type OPSRefundPlanResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type OPSRefundPlanRequest = AbstractApiRequest & {
  refundRequestId: string
}

export type MPSDeleteAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type MPSDeleteAccessoryRequest = AbstractApiRequest & {
  codeAccessory?: string[] | undefined
}

export type MPSDeleteImageAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type MPSDeleteImageAccessoryRequest = AbstractApiRequest & {
  imageId: string[]
}

export type MPSInsertAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type MPSSelectAccessoriesResponse = AbstractApiResponseOfListOfMPSSelectAccessoriesEntity & {
  response?: MPSSelectAccessoriesEntity[] | undefined
}

export type MPSSelectAccessoriesEntity = {
  accessoryId?: string | undefined
  accessoryName?: string | undefined
  description?: string | null | undefined
  shortDescription?: string | null | undefined
  price?: number | undefined
  discount?: number | null | undefined
  quantity?: number | undefined
  createdAt?: string | null | undefined
  childCategoryName?: string | null | undefined
  parentCategoryName?: string | null | undefined
  averageRating?: number | null | undefined
  totalReviews?: number | null | undefined
  totalSold?: number | null | undefined
  totalOrders?: number | null | undefined
  imageAccessoriesList?: MPSSelectImageAccessories[] | undefined
}

export type MPSSelectImageAccessories = {
  imageId?: string | undefined
  accessoryId?: string | undefined
  imageUrl?: string | undefined
}

export type AbstractApiResponseOfListOfMPSSelectAccessoriesEntity = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined
  response?: MPSSelectAccessoriesEntity[] | null | undefined
}

export type MPSSelectAccessoriesRequest = AbstractApiRequest

export type MPSUpdateAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type MSPInsertImageAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type HSShowPlanResponse = AbstractApiResponseOfListOfHSShowPlanEntity & {
  response?: HSShowPlanEntity[] | undefined
}

export type HSShowPlanEntity = {
  planId?: string | undefined
  planName?: string | undefined
  description?: string | null | undefined
  price?: number | undefined
  durationMonths?: number | undefined
}

export type AbstractApiResponseOfListOfHSShowPlanEntity = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined
  response?: HSShowPlanEntity[] | null | undefined
}

export type HSShowPlanRequest = AbstractApiRequest

export type DPSDeleteCartItemReponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type DPSDeleteCartItemRequest = AbstractApiRequest & {
  codeAccessory?: string | undefined
}

export type DPSDeleteWishListResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type DPSDeleteWishListRequest = AbstractApiRequest & {
  codeAccessory?: string | undefined
}

export type DPSInsertCartResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type DPSInsertCartRequest = AbstractApiRequest & {
  codeAccessory: string
  quantity?: number | undefined
}

export type DPSInsertWishListResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type DPSInsertWishListRequest = AbstractApiRequest & {
  codeAccessory?: string | undefined
}

export type DPSSelectAccessoryResponse = AbstractApiResponseOfDPSSelectAccessoryEntity & {
  response?: DPSSelectAccessoryEntity | undefined
}

export type DPSSelectAccessoryEntity = {
  codeAccessory?: string | undefined
  nameAccessory?: string | undefined
  shortDescription?: string | null | undefined
  description?: string | null | undefined
  price?: string | undefined
  discount?: string | undefined
  salePrice?: string | null | undefined
  createdAt?: string | null | undefined
  childCategoryName?: string | null | undefined
  parentCategoryName?: string | null | undefined
  wishlistId?: string | null | undefined
  imageUrls?: DpsSelectAccessoryListImageUrl[] | undefined
  averageRating?: number | null | undefined
  totalReviews?: number | null | undefined
}

export type DpsSelectAccessoryListImageUrl = {
  imageUrl?: string | undefined
}

export type AbstractApiResponseOfDPSSelectAccessoryEntity = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined

  response?: DPSSelectAccessoryEntity | null | undefined
}

export type DPSSelectAccessoryRequest = AbstractApiRequest & {
  codeAccessory?: string | undefined
}

export type DPSSelectCartItemResponse = AbstractApiResponseOfListOfDPSSelectCartItemEntity & {
  response?: DPSSelectCartItemEntity[] | undefined
}

export type DPSSelectCartItemEntity = {
  accessoryId?: string | undefined
  accessoryName?: string | undefined
  price?: number | undefined
  quantity?: number | undefined
  totalPrice?: number | null | undefined
  shortDescription?: string | undefined
  images?: DPSSelectCartItemImages[] | undefined
}

export type DPSSelectCartItemImages = {
  imageUrl?: string | undefined
}

export type AbstractApiResponseOfListOfDPSSelectCartItemEntity = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined
  response?: DPSSelectCartItemEntity[] | null | undefined
}

export type DPSSelectCartItemRequest = AbstractApiRequest

export type DPSSelectItemResponse = AbstractApiResponseOfDPSSelectItemEntity & {
  response?: DPSSelectItemEntity | undefined
}

export type DPSSelectItemEntity = {
  totalRecords?: number | undefined
  totalPages?: number | undefined
  items?: ItemEntity[] | undefined
}

export type ItemEntity = {
  codeProduct?: string | undefined
  nameAccessory?: string | undefined
  shortDescription?: string | null | undefined
  description?: string | null | undefined
  price?: string | undefined
  discount?: string | undefined
  salePrice?: string | undefined
  createdAt?: string | null | undefined
  childCategoryName?: string | null | undefined
  parentCategoryName?: string | null | undefined
  imageUrl?: DpsSelectItemListImageUrl[] | undefined
  averageRating?: number | null | undefined
  totalReviews?: number | null | undefined
  exchangeId?: string | null | undefined
  firstNameCreator?: string | undefined
  lastNameCreator?: string | undefined
}

export type DpsSelectItemListImageUrl = {
  imageUrl?: string | undefined
}

export type AbstractApiResponseOfDPSSelectItemEntity = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined

  response?: DPSSelectItemEntity | null | undefined
}

export type DPSSelectItemRequest = AbstractApiRequest & {
  searchBy?: number | undefined
  sortBy?: number | null | undefined
  minimumPrice?: number | null | undefined
  maximumPrice?: number | null | undefined
  nameAccessory?: string | null | undefined
  childCategoryName?: string | null | undefined
  parentCategoryName?: string | null | undefined
  currentPage?: number | undefined
  pageSize?: number | undefined
}

export type DPSSelectWishListResponse = AbstractApiResponseOfListOfDPSSelectWishListEntity & {
  response?: DPSSelectWishListEntity[] | undefined
}

export type DPSSelectWishListEntity = {
  accessoryId?: string | undefined
  accessoryName?: string | undefined
  shortDescription?: string | null | undefined
  images?: DPSSelectWishListImages[] | undefined
}

export type DPSSelectWishListImages = {
  imageUrl?: string | undefined
}

export type AbstractApiResponseOfListOfDPSSelectWishListEntity = {
  success?: boolean | undefined
  messageId?: string | undefined
  message?: string | undefined
  detailErrorList?: DetailError[] | undefined
  response?: DPSSelectWishListEntity[] | null | undefined
}

export type DPSSelectWishListRequest = AbstractApiRequest

export type DPSUpdateCartItemResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type DPSUpdateCartItemRequest = AbstractApiRequest & {
  quantity?: number | undefined
  accessoryId?: string | undefined
}

export type AEPSAddExchangeAccessoryResponse = AbstractApiResponseOfString & {
  response?: string | undefined
}

export type AEPSAddExchangeAccessoryRequest = AbstractApiRequest & {
  name: string
  description: string
  price: number
  imageUrls: string[]
}
