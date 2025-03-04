/* eslint-disable */
export type UserVerifyKeyResponse = AbstractApiResponseOfString & {
  response: string
}

export type AbstractApiResponseOfString = {
  success: boolean
  messageId: string
  message: string
  detailErrorList: DetailError[]
  response?: string | undefined
}

export type DetailError = {
  field: string
  messageId: string
  errorMessage: string
}

export type UserVerifyKeyRequest = AbstractApiRequest & {
  key: string
}

export type AbstractApiRequest = {
  isOnlyValidation: boolean
}

export type UpdateRoleResponse = AbstractApiResponseOfString & {
  response: string
}

export type UpdateRoleRequest = AbstractApiRequest & {
  userName: string
  planId?: string | undefined
}

export type FPSVerifyKeyResponse = AbstractApiResponseOfString & {
  response: string
}

export type FPSVerifyKeyRequest = AbstractApiRequest & {
  key: string
  newPassWord: string
}

export type VerifyTokenResponse = AbstractApiResponseOfVerifyTokenEntity & {
  response: VerifyTokenEntity
}

export type VerifyTokenEntity = {
  roleName: string
}

export type AbstractApiResponseOfVerifyTokenEntity = {
  success: boolean
  messageId: string
  message: string
  detailErrorList: DetailError[]
  response?: VerifyTokenEntity | undefined
}

export type VerifyTokenRequest = AbstractApiRequest

export type UserInsertResponse = AbstractApiResponseOfString & {
  response: string
}

export type UserInsertRequest = AbstractApiRequest & {
  username: string
  email: string
  password: string
  firstName: string
  lastName: string
}

export type UserInsertVerifyResponse = AbstractApiResponseOfString & {
  response: string
}

export type UserInsertVerifyRequest = AbstractApiRequest & {
  key: string
}

export type FPSForgotPasswordResponse = AbstractApiResponseOfString & {
  response: string
}

export type FPSForgotPasswordRequest = AbstractApiRequest & {
  email: string
}
export type TokenResponse = {
  access_token: string
  token_type: string
  expires_in: number
  scope: string
  refresh_token: string
}
