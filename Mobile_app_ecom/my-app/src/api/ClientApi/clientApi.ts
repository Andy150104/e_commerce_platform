/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export type URSUserRegisterResponse = AbstractApiResponseOfObject & {
  response: any;
};

export interface AbstractApiResponseOfObject {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: any;
}

export interface DetailError {
  field: string;
  messageId: string;
  errorMessage: string;
}

export type URSUserRegisterRequest = AbstractApiRequest & {
  /** @pattern ^\+?[1-9]\d{1,14}$ */
  phoneNumber?: string;
  /** @pattern ^\d{4}-\d{2}-\d{2}$ */
  birthDay?: string;
  /** @format uri */
  imageUrl?: string;
  /**
   * @format byte
   * @min 1
   * @max 3
   */
  gender?: number;
  /** @pattern ^[\p{L}0-9''-'\s]{1,40}$ */
  addressLine?: string;
  /** @pattern ^[\p{L}0-9''-'\s]{1,40}$ */
  ward?: string;
  /** @pattern ^[\p{L}\s]{1,40}$ */
  city?: string;
  /** @pattern ^[\p{L}\s]{1,40}$ */
  province?: string;
  /** @pattern ^[\p{L}0-9''-'\s]{1,40}$ */
  district?: string;
  /** @minLength 1 */
  key: string;
};

export interface AbstractApiRequest {
  isOnlyValidation: boolean;
}

export type UDSInsertUserAddressResponse = AbstractApiResponseOfString & {
  response: string;
};

export interface AbstractApiResponseOfString {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: string;
}

export type UDSInsertUserAddressRequest = AbstractApiRequest & {
  /**
   * @minLength 1
   * @pattern ^[a-zA-Z0-9''-'\s]{1,40}$
   */
  addressLine: string;
  /**
   * @minLength 1
   * @pattern ^[a-zA-Z0-9''-'\s]{1,40}$
   */
  ward: string;
  /**
   * @minLength 1
   * @pattern ^[a-zA-Z''-'\s]{1,40}$
   */
  city: string;
  /**
   * @minLength 1
   * @pattern ^[a-zA-Z''-'\s]{1,40}$
   */
  province: string;
  /**
   * @minLength 1
   * @pattern ^[a-zA-Z0-9''-'\s]{1,40}$
   */
  district: string;
};

export type UDSSelectUserAddressResponse = AbstractApiResponseOfListOfUDSSelectUserAddressEntity & {
  response: UDSSelectUserAddressEntity[];
};

export interface UDSSelectUserAddressEntity {
  /** @format guid */
  addressId: string;
  firstName: string;
  lastName: string;
  addressLine?: string;
  ward?: string;
  city?: string;
  province?: string;
  district?: string;
}

export interface AbstractApiResponseOfListOfUDSSelectUserAddressEntity {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: UDSSelectUserAddressEntity[];
}

export type UDSSelectUserAddressRequest = AbstractApiRequest & object;

export type UDSSelectUserProfileResponse = AbstractApiResponseOfUDSSelectUserProfileEntity & {
  response: UDSSelectUserProfileEntity;
};

export interface UDSSelectUserProfileEntity {
  email?: string;
  firstName?: string;
  lastName?: string;
  phoneNumber?: string;
  imageUrl?: string;
  /** @format date */
  birthDate?: string;
  /** @format byte */
  gender?: number;
}

export interface AbstractApiResponseOfUDSSelectUserProfileEntity {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: UDSSelectUserProfileEntity;
}

export type UDSSelectUserProfileRequest = AbstractApiRequest & object;

export type UDSUpdateUserAddressResponse = AbstractApiResponseOfString & {
  response: string;
};

export type UDSUpdateUserAddressRequest = AbstractApiRequest & {
  /** @format guid */
  addressId: string;
  /** @pattern ^[a-zA-Z0-9''-'\s]{1,40}$ */
  addressLine?: string;
  /** @pattern ^[a-zA-Z0-9''-'\s]{1,40}$ */
  ward?: string;
  /** @pattern ^[a-zA-Z''-'\s]{1,40}$ */
  city?: string;
  /** @pattern ^[a-zA-Z''-'\s]{1,40}$ */
  province?: string;
  /** @pattern ^[a-zA-Z0-9''-'\s]{1,40}$ */
  district?: string;
};

export type UDSUpdateUserProfileReponse = AbstractApiResponseOfString & {
  response: string;
};

export type UDSUpdateUserProfileRequest = AbstractApiRequest & {
  /** @pattern ^[a-zA-Z''-'\s]{1,40}$ */
  firstName: string;
  /** @pattern ^[a-zA-Z''-'\s]{1,40}$ */
  lastName: string;
  /** @pattern ^\+?[1-9]\d{1,14}$ */
  phoneNumber?: string;
  /** @pattern ^\d{4}-\d{2}-\d{2}$ */
  birthDay?: string;
  /** @format uri */
  imageUrl?: string;
  /**
   * @format byte
   * @min 1
   * @max 3
   */
  gender?: number;
};

export type DPSDeleteCartItemReponse = AbstractApiResponseOfString & {
  response: string;
};

export type DPSDeleteCartItemRequest = AbstractApiRequest & {
  codeProduct: string;
};

export type DPSDeleteWishListResponse = AbstractApiResponseOfString & {
  response: string;
};

export type DPSDeleteWishListRequest = AbstractApiRequest & {
  codeProduct: string;
};

export type DPSInsertCartResponse = AbstractApiResponseOfString & {
  response: string;
};

export type DPSInsertCartRequest = AbstractApiRequest & {
  codeProduct: string;
  /** @format int32 */
  quantity: number;
};

export type DPSInsertWishListResponse = AbstractApiResponseOfString & {
  response: string;
};

export type DPSInsertWishListRequest = AbstractApiRequest & {
  codeProduct: string;
};

export type DPSSelectCartItemResponse = AbstractApiResponseOfListOfDPSSelectCartItemEntity & {
  response: DPSSelectCartItemEntity[];
};

export interface DPSSelectCartItemEntity {
  productId: string;
  productName: string;
  /** @format decimal */
  price: number;
  /** @format int32 */
  quantity: number;
  /** @format decimal */
  totalPrice?: number;
  shortDescription: string;
  images: DPSSelectCartItemImages[];
}

export interface DPSSelectCartItemImages {
  imageUrl: string;
}

export interface AbstractApiResponseOfListOfDPSSelectCartItemEntity {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: DPSSelectCartItemEntity[];
}

export type DPSSelectCartItemRequest = AbstractApiRequest & object;

export type DPSSelectItemResponse = AbstractApiResponseOfDPSSelectItemEntity & {
  response: DPSSelectItemEntity;
};

export interface DPSSelectItemEntity {
  /** @format int32 */
  totalRecords: number;
  /** @format int32 */
  totalPages: number;
  items: ItemEntity[];
}

export interface ItemEntity {
  codeProduct: string;
  nameProduct: string;
  shortDescription?: string;
  description?: string;
  price: string;
  discount: string;
  salePrice: string;
  /** @format date-time */
  createdAt?: string;
  childCategoryName?: string;
  parentCategoryName?: string;
  /** @format guid */
  wishlistId?: string;
  imageUrl: DpsSelectItemListImageUrl[];
  /** @format int32 */
  averageRating?: number;
  /** @format int32 */
  totalReviews?: number;
  /** @format guid */
  exchangeId?: string;
  firstNameCreator: string;
  lastNameCreator: string;
}

export interface DpsSelectItemListImageUrl {
  imageUrl: string;
}

export interface AbstractApiResponseOfDPSSelectItemEntity {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: DPSSelectItemEntity;
}

export type DPSSelectItemRequest = AbstractApiRequest & {
  /** @format byte */
  searchBy: number;
  /** @format byte */
  sortBy?: number;
  /** @format decimal */
  minimumPrice?: number;
  /** @format decimal */
  maximumPrice?: number;
  childCategoryName?: string;
  parentCategoryName?: string;
  /** @format int32 */
  currentPage: number;
  /** @format int32 */
  pageSize: number;
};

export type DPSSelectProductResponse = AbstractApiResponseOfDPSSelectProductEntity & {
  response: DPSSelectProductEntity;
};

export interface DPSSelectProductEntity {
  codeProduct: string;
  nameProduct: string;
  shortDescription?: string;
  description?: string;
  price: string;
  discount: string;
  salePrice: string;
  /** @format date-time */
  createdAt?: string;
  childCategoryName?: string;
  parentCategoryName?: string;
  /** @format guid */
  wishlistId?: string;
  imageUrls: DpsSelectProductListImageUrl[];
  /** @format int32 */
  averageRating?: number;
  /** @format int32 */
  totalReviews?: number;
}

export interface DpsSelectProductListImageUrl {
  imageUrl: string;
}

export interface AbstractApiResponseOfDPSSelectProductEntity {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: DPSSelectProductEntity;
}

export type DPSSelectProductRequest = AbstractApiRequest & {
  codeProduct: string;
};

export type DPSSelectWishListResponse = AbstractApiResponseOfListOfDPSSelectWishListEntity & {
  response: DPSSelectWishListEntity[];
};

export interface DPSSelectWishListEntity {
  productId: string;
  productName: string;
  shortDescription?: string;
  images: DPSSelectWishListImages[];
}

export interface DPSSelectWishListImages {
  imageUrl: string;
}

export interface AbstractApiResponseOfListOfDPSSelectWishListEntity {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: DPSSelectWishListEntity[];
}

export type DPSSelectWishListRequest = AbstractApiRequest & object;

export type DPSUpdateCartItemResponse = AbstractApiResponseOfString & {
  response: string;
};

export type DPSUpdateCartItemRequest = AbstractApiRequest & {
  /** @format int32 */
  quantity: number;
  productId: string;
};

import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, HeadersDefaults, ResponseType } from "axios";
import axios from "axios";

export type QueryParamsType = Record<string | number, any>;

export interface FullRequestParams extends Omit<AxiosRequestConfig, "data" | "params" | "url" | "responseType"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseType;
  /** request body */
  body?: unknown;
}

export type RequestParams = Omit<FullRequestParams, "body" | "method" | "query" | "path">;

export interface ApiConfig<SecurityDataType = unknown> extends Omit<AxiosRequestConfig, "data" | "cancelToken"> {
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<AxiosRequestConfig | void> | AxiosRequestConfig | void;
  secure?: boolean;
  format?: ResponseType;
}

export enum ContentType {
  Json = "application/json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public instance: AxiosInstance;
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private secure?: boolean;
  private format?: ResponseType;

  constructor({ securityWorker, secure, format, ...axiosConfig }: ApiConfig<SecurityDataType> = {}) {
    this.instance = axios.create({ ...axiosConfig, baseURL: axiosConfig.baseURL || "https://localhost:5092" });
    this.secure = secure;
    this.format = format;
    this.securityWorker = securityWorker;
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected mergeRequestParams(params1: AxiosRequestConfig, params2?: AxiosRequestConfig): AxiosRequestConfig {
    const method = params1.method || (params2 && params2.method);

    return {
      ...this.instance.defaults,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...((method && this.instance.defaults.headers[method.toLowerCase() as keyof HeadersDefaults]) || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected stringifyFormItem(formItem: unknown) {
    if (typeof formItem === "object" && formItem !== null) {
      return JSON.stringify(formItem);
    } else {
      return `${formItem}`;
    }
  }

  protected createFormData(input: Record<string, unknown>): FormData {
    if (input instanceof FormData) {
      return input;
    }
    return Object.keys(input || {}).reduce((formData, key) => {
      const property = input[key];
      const propertyContent: any[] = property instanceof Array ? property : [property];

      for (const formItem of propertyContent) {
        const isFileType = formItem instanceof Blob || formItem instanceof File;
        formData.append(key, isFileType ? formItem : this.stringifyFormItem(formItem));
      }

      return formData;
    }, new FormData());
  }

  public request = async <T = any, _E = any>({
    secure,
    path,
    type,
    query,
    format,
    body,
    ...params
  }: FullRequestParams): Promise<AxiosResponse<T>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const responseFormat = format || this.format || undefined;

    if (type === ContentType.FormData && body && body !== null && typeof body === "object") {
      body = this.createFormData(body as Record<string, unknown>);
    }

    if (type === ContentType.Text && body && body !== null && typeof body !== "string") {
      body = JSON.stringify(body);
    }

    return this.instance.request({
      ...requestParams,
      headers: {
        ...(requestParams.headers || {}),
        ...(type ? { "Content-Type": type } : {}),
      },
      params: query,
      responseType: responseFormat,
      data: body,
      url: path,
    });
  };
}

/**
 * @title My Title
 * @version 1.0.0
 * @baseUrl https://localhost:5092
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags URSUserRegister
     * @name UrsUserRegisterPost
     * @request POST:/api/v1/URSUserRegister
     * @secure
     */
    ursUserRegisterPost: (request: URSUserRegisterRequest, params: RequestParams = {}) =>
      this.request<URSUserRegisterResponse, any>({
        path: `/api/v1/URSUserRegister`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UDSInsertUserAddress
     * @name UdsInsertUserAddressPost
     * @request POST:/api/v1/UDSInsertUserAddress
     * @secure
     */
    udsInsertUserAddressPost: (request: UDSInsertUserAddressRequest, params: RequestParams = {}) =>
      this.request<UDSInsertUserAddressResponse, any>({
        path: `/api/v1/UDSInsertUserAddress`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UDSSelectUserAddress
     * @name UdsSelectUserAddressPost
     * @request POST:/api/v1/UDSSelectUserAddress
     * @secure
     */
    udsSelectUserAddressPost: (request: UDSSelectUserAddressRequest, params: RequestParams = {}) =>
      this.request<UDSSelectUserAddressResponse, any>({
        path: `/api/v1/UDSSelectUserAddress`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UDSSelectUserProfile
     * @name UdsSelectUserProfilePost
     * @request POST:/api/v1/UDSSelectUserProfile
     * @secure
     */
    udsSelectUserProfilePost: (request: UDSSelectUserProfileRequest, params: RequestParams = {}) =>
      this.request<UDSSelectUserProfileResponse, any>({
        path: `/api/v1/UDSSelectUserProfile`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UDSUpdateUserAddress
     * @name UdsUpdateUserAddressPost
     * @request POST:/api/v1/UDSUpdateUserAddress
     * @secure
     */
    udsUpdateUserAddressPost: (request: UDSUpdateUserAddressRequest, params: RequestParams = {}) =>
      this.request<UDSUpdateUserAddressResponse, any>({
        path: `/api/v1/UDSUpdateUserAddress`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UDSUpdateUserProfile
     * @name UdsUpdateUserProfilePost
     * @request POST:/api/v1/UDSUpdateUserProfile
     * @secure
     */
    udsUpdateUserProfilePost: (request: UDSUpdateUserProfileRequest, params: RequestParams = {}) =>
      this.request<UDSUpdateUserProfileReponse, any>({
        path: `/api/v1/UDSUpdateUserProfile`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags DPSDeleteCartItem
     * @name DpsDeleteCartItemPost
     * @request POST:/api/v1/DPSDeleteCartItem
     * @secure
     */
    dpsDeleteCartItemPost: (request: DPSDeleteCartItemRequest, params: RequestParams = {}) =>
      this.request<DPSDeleteCartItemReponse, any>({
        path: `/api/v1/DPSDeleteCartItem`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags DPSDeleteWishList
     * @name DpsDeleteWishListPost
     * @request POST:/api/v1/DPSDeleteWishList
     * @secure
     */
    dpsDeleteWishListPost: (request: DPSDeleteWishListRequest, params: RequestParams = {}) =>
      this.request<DPSDeleteWishListResponse, any>({
        path: `/api/v1/DPSDeleteWishList`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags DPSInsertCart
     * @name DpsInsertCartPost
     * @request POST:/api/v1/DPSInsertCart
     * @secure
     */
    dpsInsertCartPost: (request: DPSInsertCartRequest, params: RequestParams = {}) =>
      this.request<DPSInsertCartResponse, any>({
        path: `/api/v1/DPSInsertCart`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags DPSInsertWishList
     * @name DpsInsertWishListPost
     * @request POST:/api/v1/DPSInsertWishList
     * @secure
     */
    dpsInsertWishListPost: (request: DPSInsertWishListRequest, params: RequestParams = {}) =>
      this.request<DPSInsertWishListResponse, any>({
        path: `/api/v1/DPSInsertWishList`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags DPSSelectCartItem
     * @name DpsSelectCartItemPost
     * @request POST:/api/v1/DPSSelectCartItem
     * @secure
     */
    dpsSelectCartItemPost: (request: DPSSelectCartItemRequest, params: RequestParams = {}) =>
      this.request<DPSSelectCartItemResponse, any>({
        path: `/api/v1/DPSSelectCartItem`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags DPSSelectItem
     * @name DpsSelectItemPost
     * @request POST:/api/v1/DPSSelectItem
     * @secure
     */
    dpsSelectItemPost: (request: DPSSelectItemRequest, params: RequestParams = {}) =>
      this.request<DPSSelectItemResponse, any>({
        path: `/api/v1/DPSSelectItem`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags DPSSelectProduct
     * @name DpsSelectProductPost
     * @request POST:/api/v1/DPSSelectProduct
     * @secure
     */
    dpsSelectProductPost: (request: DPSSelectProductRequest, params: RequestParams = {}) =>
      this.request<DPSSelectProductResponse, any>({
        path: `/api/v1/DPSSelectProduct`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags DPSSelectWishList
     * @name DpsSelectWishListPost
     * @request POST:/api/v1/DPSSelectWishList
     * @secure
     */
    dpsSelectWishListPost: (itemRequest: DPSSelectWishListRequest, params: RequestParams = {}) =>
      this.request<DPSSelectWishListResponse, any>({
        path: `/api/v1/DPSSelectWishList`,
        method: "POST",
        body: itemRequest,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags DPSUpdateCartItem
     * @name DpsUpdateCartItemPost
     * @request POST:/api/v1/DPSUpdateCartItem
     * @secure
     */
    dpsUpdateCartItemPost: (itemRequest: DPSUpdateCartItemRequest, params: RequestParams = {}) =>
      this.request<DPSUpdateCartItemResponse, any>({
        path: `/api/v1/DPSUpdateCartItem`,
        method: "POST",
        body: itemRequest,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  };
}
