// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace System
{
    using Linq.Expressions;
    using MvvmCross.Core.ViewModels;

    public static class ViewModelExtensions
    {
        public static void OnPropertyChanged<T>(
            this MvxNotifyPropertyChanged viewModel,
            Expression<Func<T>> property,
            Action action)
        {
            if (property.Body is MemberExpression memberExpression)
            {
                viewModel.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == memberExpression.Member.Name)
                    {
                        action();
                    }
                };
            }
            else
            {
                throw new ArgumentException("Expression must be of type MemberExpression and have a body.", nameof(property));
            }
        }
    }
}
