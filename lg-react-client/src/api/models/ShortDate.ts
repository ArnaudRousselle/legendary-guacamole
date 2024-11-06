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
/**
 * 
 * @export
 * @interface ShortDate
 */
export interface ShortDate {
    /**
     * 
     * @type {number}
     * @memberof ShortDate
     */
    year: number;
    /**
     * 
     * @type {number}
     * @memberof ShortDate
     */
    month: number;
    /**
     * 
     * @type {number}
     * @memberof ShortDate
     */
    day: number;
}

/**
 * Check if a given object implements the ShortDate interface.
 */
export function instanceOfShortDate(value: object): value is ShortDate {
    if (!('year' in value) || value['year'] === undefined) return false;
    if (!('month' in value) || value['month'] === undefined) return false;
    if (!('day' in value) || value['day'] === undefined) return false;
    return true;
}

export function ShortDateFromJSON(json: any): ShortDate {
    return ShortDateFromJSONTyped(json, false);
}

export function ShortDateFromJSONTyped(json: any, ignoreDiscriminator: boolean): ShortDate {
    if (json == null) {
        return json;
    }
    return {
        
        'year': json['year'],
        'month': json['month'],
        'day': json['day'],
    };
}

export function ShortDateToJSON(value?: ShortDate | null): any {
    if (value == null) {
        return value;
    }
    return {
        
        'year': value['year'],
        'month': value['month'],
        'day': value['day'],
    };
}
