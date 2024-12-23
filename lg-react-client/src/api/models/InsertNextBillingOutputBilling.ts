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
 * @interface InsertNextBillingOutputBilling
 */
export interface InsertNextBillingOutputBilling {
    /**
     * 
     * @type {string}
     * @memberof InsertNextBillingOutputBilling
     */
    id: string;
    /**
     * 
     * @type {ShortDate}
     * @memberof InsertNextBillingOutputBilling
     */
    valuationDate: ShortDate;
    /**
     * 
     * @type {string}
     * @memberof InsertNextBillingOutputBilling
     */
    title: string;
    /**
     * 
     * @type {number}
     * @memberof InsertNextBillingOutputBilling
     */
    amount: number;
    /**
     * 
     * @type {boolean}
     * @memberof InsertNextBillingOutputBilling
     */
    checked: boolean;
    /**
     * 
     * @type {string}
     * @memberof InsertNextBillingOutputBilling
     */
    comment?: string | null;
    /**
     * 
     * @type {boolean}
     * @memberof InsertNextBillingOutputBilling
     */
    isSaving: boolean;
}

/**
 * Check if a given object implements the InsertNextBillingOutputBilling interface.
 */
export function instanceOfInsertNextBillingOutputBilling(value: object): value is InsertNextBillingOutputBilling {
    if (!('id' in value) || value['id'] === undefined) return false;
    if (!('valuationDate' in value) || value['valuationDate'] === undefined) return false;
    if (!('title' in value) || value['title'] === undefined) return false;
    if (!('amount' in value) || value['amount'] === undefined) return false;
    if (!('checked' in value) || value['checked'] === undefined) return false;
    if (!('isSaving' in value) || value['isSaving'] === undefined) return false;
    return true;
}

export function InsertNextBillingOutputBillingFromJSON(json: any): InsertNextBillingOutputBilling {
    return InsertNextBillingOutputBillingFromJSONTyped(json, false);
}

export function InsertNextBillingOutputBillingFromJSONTyped(json: any, ignoreDiscriminator: boolean): InsertNextBillingOutputBilling {
    if (json == null) {
        return json;
    }
    return {
        
        'id': json['id'],
        'valuationDate': ShortDateFromJSON(json['valuationDate']),
        'title': json['title'],
        'amount': json['amount'],
        'checked': json['checked'],
        'comment': json['comment'] == null ? undefined : json['comment'],
        'isSaving': json['isSaving'],
    };
}

export function InsertNextBillingOutputBillingToJSON(value?: InsertNextBillingOutputBilling | null): any {
    if (value == null) {
        return value;
    }
    return {
        
        'id': value['id'],
        'valuationDate': ShortDateToJSON(value['valuationDate']),
        'title': value['title'],
        'amount': value['amount'],
        'checked': value['checked'],
        'comment': value['comment'],
        'isSaving': value['isSaving'],
    };
}

