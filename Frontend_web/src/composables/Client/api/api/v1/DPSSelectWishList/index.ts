/* eslint-disable */
import type { DefineMethods } from 'aspida';
import type * as Types from '../../../@types';

export type Methods = DefineMethods<{
  get: {
    query?: {
      itemRequest?: Types.DPSSelectWishListRequest | undefined;
    } | undefined;

    status: 200;
    resBody: Types.DPSSelectWishListResponse;
  };
}>;
