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
import type { Frequence } from './Frequence';
import {
    FrequenceFromJSON,
    FrequenceFromJSONTyped,
    FrequenceToJSON,
} from './Frequence';

/**
 * 
 * @export
 * @interface GetRepetitiveBillingOutput
 */
export interface GetRepetitiveBillingOutput {
    /**
     * 
     * @type {string}
     * @memberof GetRepetitiveBillingOutput
     */
    id: string;
    /**
     * 
     * @type {ShortDate}
     * @memberof GetRepetitiveBillingOutput
     */
    nextValuationDate: ShortDate;
    /**
     * 
     * @type {string}
     * @memberof GetRepetitiveBillingOutput
     */
    title: string;
    /**
     * 
     * @type {number}
     * @memberof GetRepetitiveBillingOutput
     */
    amount: number;
    /**
     * 
     * @type {boolean}
     * @memberof GetRepetitiveBillingOutput
     */
    isSaving: boolean;
    /**
     * 
     * @type {Frequence}
     * @memberof GetRepetitiveBillingOutput
     */
    frequence: Frequence;
}



/**
 * Check if a given object implements the GetRepetitiveBillingOutput interface.
 */
export function instanceOfGetRepetitiveBillingOutput(value: object): value is GetRepetitiveBillingOutput {
    if (!('id' in value) || value['id'] === undefined) return false;
    if (!('nextValuationDate' in value) || value['nextValuationDate'] === undefined) return false;
    if (!('title' in value) || value['title'] === undefined) return false;
    if (!('amount' in value) || value['amount'] === undefined) return false;
    if (!('isSaving' in value) || value['isSaving'] === undefined) return false;
    if (!('frequence' in value) || value['frequence'] === undefined) return false;
    return true;
}

export function GetRepetitiveBillingOutputFromJSON(json: any): GetRepetitiveBillingOutput {
    return GetRepetitiveBillingOutputFromJSONTyped(json, false);
}

export function GetRepetitiveBillingOutputFromJSONTyped(json: any, ignoreDiscriminator: boolean): GetRepetitiveBillingOutput {
    if (json == null) {
        return json;
    }
    return {
        
        'id': json['id'],
        'nextValuationDate': ShortDateFromJSON(json['nextValuationDate']),
        'title': json['title'],
        'amount': json['amount'],
        'isSaving': json['isSaving'],
        'frequence': FrequenceFromJSON(json['frequence']),
    };
}

export function GetRepetitiveBillingOutputToJSON(value?: GetRepetitiveBillingOutput | null): any {
    if (value == null) {
        return value;
    }
    return {
        
        'id': value['id'],
        'nextValuationDate': ShortDateToJSON(value['nextValuationDate']),
        'title': value['title'],
        'amount': value['amount'],
        'isSaving': value['isSaving'],
        'frequence': FrequenceToJSON(value['frequence']),
    };
}

