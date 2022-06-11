#!/usr/bin/env node

const fs = require('fs');

const sourcePath = './tmp/FastNoiseLite.cs';
const destPath = '../../Runtime/Scripts/Core/FastNoiseLite.cs';

const copyFnlToAssets = () => {
	const input = fs.readFileSync(sourcePath, 'utf8');
	const output = `namespace Obsidize.FastNoise {\n${input}\n}`;
	fs.writeFileSync(destPath, output, 'utf8');
};

copyFnlToAssets();