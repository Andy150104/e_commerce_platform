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

export type UserVerifyKeyResponse = AbstractApiResponseOfString & {
  response: string;
};

export interface AbstractApiResponseOfString {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: string;
}

export interface DetailError {
  field: string;
  messageId: string;
  errorMessage: string;
}

export type UserVerifyKeyRequest = AbstractApiRequest & {
  key: string;
};

export interface AbstractApiRequest {
  isOnlyValidation: boolean;
}

export type UpdateRoleResponse = AbstractApiResponseOfString & {
  response: string;
};

export type UpdateRoleRequest = AbstractApiRequest & {
  userName: string;
  /** @format guid */
  planId?: string;
};

export type FPSVerifyKeyResponse = AbstractApiResponseOfString & {
  response: string;
};

export type FPSVerifyKeyRequest = AbstractApiRequest & {
  /** @minLength 1 */
  key: string;
  /** @minLength 1 */
  newPassWord: string;
};

export type VerifyTokenResponse = AbstractApiResponseOfVerifyTokenEntity & {
  response: VerifyTokenEntity;
};

export interface VerifyTokenEntity {
  roleName: string;
}

export interface AbstractApiResponseOfVerifyTokenEntity {
  success: boolean;
  messageId: string;
  message: string;
  detailErrorList: DetailError[];
  response?: VerifyTokenEntity;
}

export type VerifyTokenRequest = AbstractApiRequest & object;

export type UserInsertResponse = AbstractApiResponseOfString & {
  response: string;
};

export type UserInsertRequest = AbstractApiRequest & {
  /** @minLength 1 */
  username: string;
  /** @minLength 1 */
  email: string;
  /** @minLength 1 */
  password: string;
  /**
   * @minLength 1
   * @pattern ^[a-zA-Z''-'\s]{1,40}$
   */
  firstName: string;
  /**
   * @minLength 1
   * @pattern ^[a-zA-Z''-'\s]{1,40}$
   */
  lastName: string;
};

export type UserInsertVerifyResponse = AbstractApiResponseOfString & {
  response: string;
};

export type UserInsertVerifyRequest = AbstractApiRequest & {
  key: string;
};

export type FPSForgotPasswordResponse = AbstractApiResponseOfString & {
  response: string;
};

export type FPSForgotPasswordRequest = AbstractApiRequest & {
  /**
   * @format email
   * @minLength 1
   */
  email: string;
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
    this.instance = axios.create({ ...axiosConfig, baseURL: axiosConfig.baseURL || "https://localhost:5090" });
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
 * @baseUrl https://localhost:5090
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags UserVerifyKey
     * @name UserVerifyKeyPost
     * @request POST:/api/v1/UserVerifyKey
     * @secure
     */
    userVerifyKeyPost: (keyRequest: UserVerifyKeyRequest, params: RequestParams = {}) =>
      this.request<UserVerifyKeyResponse, any>({
        path: `/api/v1/UserVerifyKey`,
        method: "POST",
        body: keyRequest,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UpdateRole
     * @name UpdateRolePost
     * @request POST:/api/v1/UpdateRole
     * @secure
     */
    updateRolePost: (request: UpdateRoleRequest, params: RequestParams = {}) =>
      this.request<UpdateRoleResponse, any>({
        path: `/api/v1/UpdateRole`,
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
     * @tags FPSVerifyKey
     * @name FpsVerifyKeyPost
     * @request POST:/api/v1/FPSVerifyKey
     * @secure
     */
    fpsVerifyKeyPost: (request: FPSVerifyKeyRequest, params: RequestParams = {}) =>
      this.request<FPSVerifyKeyResponse, any>({
        path: `/api/v1/FPSVerifyKey`,
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
     * @tags VerifyToken
     * @name VerifyTokenPost
     * @request POST:/api/v1/VerifyToken
     * @secure
     */
    verifyTokenPost: (request: VerifyTokenRequest, params: RequestParams = {}) =>
      this.request<VerifyTokenResponse, any>({
        path: `/api/v1/VerifyToken`,
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
     * @tags UserInsert
     * @name UserInsertPost
     * @request POST:/api/v1/UserInsert
     * @secure
     */
    userInsertPost: (request: UserInsertRequest, params: RequestParams = {}) =>
      this.request<UserInsertResponse, any>({
        path: `/api/v1/UserInsert`,
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
     * @tags UserInsertVerify
     * @name UserInsertVerifyPost
     * @request POST:/api/v1/UserInsertVerify
     * @secure
     */
    userInsertVerifyPost: (request: UserInsertVerifyRequest, params: RequestParams = {}) =>
      this.request<UserInsertVerifyResponse, any>({
        path: `/api/v1/UserInsertVerify`,
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
     * @tags FPSForgotPassword
     * @name FpsForgotPasswordPost
     * @request POST:/api/v1/FPSForgotPassword
     * @secure
     */
    fpsForgotPasswordPost: (request: FPSForgotPasswordRequest, params: RequestParams = {}) =>
      this.request<FPSForgotPasswordResponse, any>({
        path: `/api/v1/FPSForgotPassword`,
        method: "POST",
        body: request,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  };
  connect = {
    /**
     * No description
     *
     * @tags Authentication
     * @name AuthenticationExchange
     * @request POST:/connect/token
     * @secure
     */
    authenticationExchange: (
      query?: {
        UserNameOrEmail?: string;
        Password?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<File | null, any>({
        path: `/connect/token`,
        method: "POST",
        query: query,
        secure: true,
        ...params,
      }),
  };
  loginWithGoogle = {
    /**
     * No description
     *
     * @tags GoogleLoginControlller
     * @name GoogleLoginControlllerLoginWithGoogle
     * @request GET:/LoginWithGoogle
     * @secure
     */
    googleLoginControlllerLoginWithGoogle: (params: RequestParams = {}) =>
      this.request<File | null, any>({
        path: `/LoginWithGoogle`,
        method: "GET",
        secure: true,
        ...params,
      }),
  };
  googleResponse = {
    /**
     * No description
     *
     * @tags GoogleLoginControlller
     * @name GoogleLoginControlllerGoogleResponse
     * @request GET:/GoogleResponse
     * @secure
     */
    googleLoginControlllerGoogleResponse: (params: RequestParams = {}) =>
      this.request<File | null, any>({
        path: `/GoogleResponse`,
        method: "GET",
        secure: true,
        ...params,
      }),
  };
}
