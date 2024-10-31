/* tslint:disable */
/* eslint-disable */
/**
 * LegendaryGuacamole.WebApi
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { mapValues } from '../runtime';
import type { ShortDate } from './ShortDate';
import {
    ShortDateFromJSON,
    ShortDateFromJSONTyped,
    ShortDateToJSON,
} from './ShortDate';

/**
 * 
 * @export
 * @interface ListBillingsInput
 */
export interface ListBillingsInput {
    /**
     * 
     * @type {ShortDate}
     * @memberof ListBillingsInput
     */
    startDate?: ShortDate;
    /**
     * 
     * @type {ShortDate}
     * @memberof ListBillingsInput
     */
    endDate?: ShortDate;
    /**
     * 
     * @type {number}
     * @memberof ListBillingsInput
     */
    amount?: number | null;
    /**
     * 
     * @type {number}
     * @memberof ListBillingsInput
     */
    deltaAmount?: number | null;
    /**
     * 
     * @type {string}
     * @memberof ListBillingsInput
     */
    title?: string | null;
    /**
     * 
     * @type {boolean}
     * @memberof ListBillingsInput
     */
    withArchived?: boolean | null;
}

/**
 * Check if a given object implements the ListBillingsInput interface.
 */
export function instanceOfListBillingsInput(value: object): value is ListBillingsInput {
    return true;
}

export function ListBillingsInputFromJSON(json: any): ListBillingsInput {
    return ListBillingsInputFromJSONTyped(json, false);
}

export function ListBillingsInputFromJSONTyped(json: any, ignoreDiscriminator: boolean): ListBillingsInput {
    if (json == null) {
        return json;
    }
    return {
        
        'startDate': json['startDate'] == null ? undefined : ShortDateFromJSON(json['startDate']),
        'endDate': json['endDate'] == null ? undefined : ShortDateFromJSON(json['endDate']),
        'amount': json['amount'] == null ? undefined : json['amount'],
        'deltaAmount': json['deltaAmount'] == null ? undefined : json['deltaAmount'],
        'title': json['title'] == null ? undefined : json['title'],
        'withArchived': json['withArchived'] == null ? undefined : json['withArchived'],
    };
}

export function ListBillingsInputToJSON(value?: ListBillingsInput | null): any {
    if (value == null) {
        return value;
    }
    return {
        
        'startDate': ShortDateToJSON(value['startDate']),
        'endDate': ShortDateToJSON(value['endDate']),
        'amount': value['amount'],
        'deltaAmount': value['deltaAmount'],
        'title': value['title'],
        'withArchived': value['withArchived'],
    };
}

