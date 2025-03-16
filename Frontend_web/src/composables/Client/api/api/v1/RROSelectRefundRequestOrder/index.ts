/* eslint-disable */
import type { DefineMethods } from 'aspida';
import type * as Types from '../../../@types';

export type Methods = DefineMethods<{
  get: {
    query?: {
      RefundId?: string | undefined;
    } | undefined;

    status: 200;
    resBody: Types.RROSelectRefundRequestOrderResponse;
  };
}>;
