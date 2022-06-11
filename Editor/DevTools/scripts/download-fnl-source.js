#!/usr/bin/env node

const https = require('https');
const fs = require('fs');

const sourceUrl = 'https://raw.githubusercontent.com/Auburn/FastNoiseLite/master/CSharp/FastNoiseLite.cs';
const destPath = './tmp/FastNoiseLite.cs';

const httpsGet = (url) => {
	return new Promise(resolve => https.get(url, resolve));
};

const writeMessageToFile = (incomingMessage, dest) => {
	return new Promise((resolve, reject) => {

		const writeStream = fs.createWriteStream(dest);

		const closeAnd = (action) => {
			writeStream.close();
			action();
		};

		writeStream.on('error', () => closeAnd(reject));
		writeStream.on('finish', () => closeAnd(resolve));

		incomingMessage.pipe(writeStream);
	});
};

const downloadSourceFile = async () => {
	const incomingMessage = await httpsGet(sourceUrl);
	await writeMessageToFile(incomingMessage, destPath);
};

downloadSourceFile().catch(e => {
	console.log('ERROR: ', e);
});