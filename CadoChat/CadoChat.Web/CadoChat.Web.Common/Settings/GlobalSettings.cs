﻿using CadoChat.Web.Common.Settings.Service;
using CadoChat.Web.Common.Settings.User;

namespace CadoChat.Web.Common.Settings
{

    /// <summary>
    /// Настройки окружения для межсервисного взаимодействия
    /// </summary>
    public class GlobalSettings
    {

        /// <summary>
        /// Приватный ключ для шифрования JWT токенов
        /// </summary>
        public readonly string _privateKey = "MIIEpAIBAAKCAQEAtC1E0SnF93EgjBahw/0yQ4BZpPZkeYBwp2+pHBQMBgQjgCxzxE8jhcDfOSr1KQWVUvfbWt2cVJIuLude382C2meN4TD9wKTqWhB+MgwKRJWicS2+uQS9/05LsJUR4czPLvLEMgUqhqxWiadb8slw8vaVJ3utFRNBHUJNoeOT9Ztwcy7qobq0YrbDG+DCReubYdyjbdH2ITykPZf2yNp2YX7wFG1QH0lVU4ItQoNBsTyXEyee9akvWMSiZATdWOtm4+zaMZIJGyVX6VL3EAsQ1V5QLGXyTA1N74BonBB9+NHHdy+WdoQe/JLSpGBj6syKFwQtsgrMCBxlUGkxeYgWCQIDAQABAoIBABerwti/5jRF9oKxDnuTLiFUIXLctAKKb0JwFwWLVLENpiRWsrbdtssBtdHq5N6Izz9hNL5RUxKBSfP7jalVdJWA+VDWgN/oSqmedRXaIxczmW3JFr9z8goynRsL2peRsr52QnRX3WhoB8554EibUm15G8teIjUcnHddmJlmLrAbjQC1gk0quvCk7ggFmz0r5CoccECslO+CXhZ9rFXKZ1PFbtxZ8yIMlQ3ghK+3yMf8tM0NBDxk9Hjsq1titNp6P6nyEqK1dJ1CmUfcEzducnzVHcgLNo+vZfLS9Oysv+BoWuzWIA62kZld7A+G5w3wEp+OEHAgR28khwBRk2FYKqkCgYEA7skVPHPy9W7cdwE+30P4bvPg/LYpEOAjdyqXVtm76QECjiktYaTuxiMH6RKMLUA8AVxqJYHg7ABYtA7+Qb15kyeIqRIOFq5G109BIWfoZD/eLx9B8yOgPST0uVWc7P4TDXnIDs8H6RLUGft4TJA9T/tAojDNKA1PON/gdAKGGFcCgYEAwSqH9ZO7uwvhePjtEsuBUQBANFTr/Abm8IM4rSbB3xPfE6sfHy4vxUoeu1mEiLQngu4Md1Q64yInp+xOBOLFUBKvGfe66ddG2UBqG/W/jBPggTt+i4ehw1w1tGZ0JQPG5Kac2BYh33OpCInldRsx6JeNExTBA3B8PIeW9hu9yJ8CgYEA4cGZY0tYdDT5GUZDNADmO7g1iZeLodnXjg3lgYZfw35h9Rf3QO8XlJqAGxqfDxVA5iSCcq2lglsdgjb+qhbCf58L9JUOXuEsNtpGgJflvgooPTL3PjH7iHONMEBCGkpopv/xZhbUqsZTY7E93l0sqpaoV+99t5VFxkbbxbKxJwcCgYEAmprV8wJpUU4zCsYByfdD63cN7FTEBBXqJTqB1GSe61NWSsG9yREIfxnR+xWs9FVtAmhRZfjuoPinUMnbsCFo16v8pgYXfi4lsKDTzMkmpJEMMaNSp47JNDnLajZOY4ngWQXZp0Ifnl9OPV1RYCeCDK2v5kPIMF6JsVC8zQJrJfUCgYAJe5Ag0yNKwPaCQ4Fm08s2HfzVFRSLBWKyutr9351+bpkfUyXjBRSDKjJNSTXkt7VhWJpW+ocQjP1mChk8ov0VGK8p/5gYBuy4HdY/ddjqY5icFHgZYHyrXg/072h60IyFc4CO6/7eE7u+I+xNKiy2Vt85E3efo4PM0jHhTgzyYg==";

        /// <summary>
        /// Конфигурация сервисов
        /// </summary>
        public ServiceObjects Services { get; set; } = null!;

        /// <summary>
        /// Публичный ключ для шифрования JWT токенов
        /// </summary>
        public string SecretKey { get; set; } = null!;

        /// <summary>
        /// Конфигурация пользователей
        /// </summary>
        public UserObjects Users { get; set; } = null!;
    }
}
