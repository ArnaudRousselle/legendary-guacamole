/* eslint-disable */
import fs from "fs";
import pkg from "glob";

const { sync: globSync } = pkg;
const { readFileSync, rmSync } = fs;

const APIS_PATH = "./src/api/";
const FILE_PATH = "./src/api/.openapi-generator/FILES";

const files = readFileSync(FILE_PATH, { encoding: "utf-8", flag: "r" })
  .split("\n")
  .map((f) => f.replace("\r", ""));

globSync(APIS_PATH + "**/*.ts")
  .map((path) => path.replace(APIS_PATH, ""))
  .filter((f) => !files.includes(f))
  .forEach((f) => {
    console.log(`deleting ${f}...`);
    rmSync(APIS_PATH + f);
  });

console.log("clean-api done!");
process.exit(0);
