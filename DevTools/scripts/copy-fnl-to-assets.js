#!/usr/bin/env node

const fs = require('fs');

const sourcePath = '../tmp/FastNoiseLite.cs';
const destPath = '../Runtime/FastNoiseLite.cs';

const copyFnlToAssets = () => {
	const input = fs.readFileSync(sourcePath, 'utf8');
	const output = `namespace obsidize.FastNoise {\n${input}\n}`;
	fs.writeFileSync(destPath, output, 'utf8');
};

copyFnlToAssets();