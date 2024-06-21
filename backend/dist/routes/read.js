"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const fs_1 = __importDefault(require("fs"));
const router = (0, express_1.Router)();
const dbFilePath = './db.json';
router.get('/', (req, res) => {
    const index = parseInt(req.query.index, 10);
    if (isNaN(index) || index < 0) {
        return res.status(400).json({ error: 'Invalid index parameter' });
    }
    const dbData = JSON.parse(fs_1.default.readFileSync(dbFilePath, 'utf-8'));
    const submission = dbData.submissions[index];
    if (!submission) {
        return res.status(404).json({ error: 'Submission not found' });
    }
    res.json(submission);
});
exports.default = router;
