/* eslint-disable */
export type UserInsertResponse = AbstractApiResponseOfString & {
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

export type UserInsertRequest = {
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

export type AbstractApiRequest = {
  isOnlyValidation: boolean
}
export type TokenResponse = {
  access_token: string
  token_type: string
  expires_in: number
  scope: string
  refresh_token: string
}
